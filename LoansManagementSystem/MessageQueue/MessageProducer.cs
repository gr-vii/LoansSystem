using LoansManagementSystem.Utilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace LoansManagementSystem.MessageQueue;

public class MessageProducer : IMessageProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly Configurations _config;

    public MessageProducer(IOptions<Configurations> config)
    {
        _config = config.Value;

        var factory = new ConnectionFactory()
        {
            HostName = _config.RabbitMqHostName,
            UserName = _config.RabbitMqUserName,
            Password = _config.RabbitMqPassword,
            VirtualHost = _config.RabbitMqVirtualHost
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("loanApplications", true, false, false);
    }

    public void SendMessage<T>(T message)
    {
        var jsonString = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;

        _channel.BasicPublish("", "loanApplications", properties, body);
    }
}
