using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LoansProcessingSystem;

public class MessageConsumer : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageConsumer()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "admin",
            VirtualHost = "/"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("loanApplications", true, false, false);
        _channel.QueueDeclare("validationResults", true, false, false);
    }

    public void StartConsuming()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var stringMessage = Encoding.UTF8.GetString(body);
            var message = JsonConvert.DeserializeObject<LoanApplication>(stringMessage);

            Console.WriteLine("Received {0}", stringMessage);
            Console.WriteLine("Received {0}", message);

            //Thread.Sleep(3000);
            //TODO: return id and resutl and errors
            var validationResult = LoanApplicationValidator.Validate(message);

            var validationResultJson = JsonConvert.SerializeObject(validationResult);

            var validationResultBody = Encoding.UTF8.GetBytes(validationResultJson);

            _channel.BasicPublish(exchange: "", routingKey: "validationResults", basicProperties: null, body: validationResultBody);

            Console.WriteLine("Sent {0}", message);
        };

        _channel.BasicConsume(queue: "loanApplications", true, consumer);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}