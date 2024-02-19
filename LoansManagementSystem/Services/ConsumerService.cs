using LoansManagementSystem.MessageQueue;

namespace LoansManagementSystem.Services;

public class ConsumerService : BackgroundService
{
    private readonly IMessageConsumer _consumer;

    public ConsumerService(IMessageConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _consumer.ReceiveMessage();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return Task.CompletedTask;
    }
}