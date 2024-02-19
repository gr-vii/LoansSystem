using LoansManagementSystem.MessageQueue;

namespace LoansManagementSystem.BackgroundServices;

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
            Console.WriteLine("hiiiiiiiiiiiiiiiiiiiiiii");

            _consumer.ReceiveMessage();
        }
        catch (Exception e)
        {
            Console.WriteLine("hiiiiiiiiiiiiiiiiiiiiiii");
            Console.WriteLine(e.Message);
        }

        return Task.CompletedTask;
    }
}