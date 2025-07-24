
namespace KafkaAndQueueConsumer.Service
{
    public interface IKafkaMessageHandler
    {
        Task HandleAsync(Person message);
    }
}
