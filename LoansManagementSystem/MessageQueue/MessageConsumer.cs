using LoansManagementSystem.Api.Commands.LoanApplications;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LoansManagementSystem.MessageQueue;

public class MessageConsumer : IMessageConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly Configurations _config;
    protected readonly IMediator _mediator;

    public MessageConsumer(IOptions<Configurations> config, IMediator mediator)
    {
        _config = config.Value;
        _mediator = mediator;

        var factory = new ConnectionFactory()
        {
            HostName = _config.RabbitMqHostName,
            UserName = _config.RabbitMqUserName,
            Password = _config.RabbitMqPassword,
            //TODO FIX THIS RASING ERROR
            VirtualHost = _config.RabbitMqVirtualHost
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("validationResults", true, false, false);
    }

    public async void ReceiveMessage()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var stringMessage = Encoding.UTF8.GetString(body);
            var deserializedMessage = JsonConvert.DeserializeObject<SetStatusClientLoanApplicationRequest>(stringMessage);

            var command = new SetStatusClientLoanApplicationInfoRequest(deserializedMessage);
            var result = _mediator.Send(command);
            Console.WriteLine("hiiiiiiiiiiiiiiiiiiiiiii");

            Console.WriteLine("Record update result: {0}", result);
        };

        _channel.BasicConsume(queue: "validationResults", autoAck: true, consumer: consumer);
    }
}
