namespace LoansManagementSystem.MessageQueue;

public interface IMessageProducer
{
    public void SendMessage<T>(T message);
}