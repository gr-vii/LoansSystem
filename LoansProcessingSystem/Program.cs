namespace LoansProcessingSystem;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            using var consumer = new MessageConsumer();
            //lifetime.ApplicationStarted.Register(consumer.StartConsuming);
            consumer.StartConsuming();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Console.WriteLine("Press [enter] to exit.");
        Console.ReadLine();
    }
}