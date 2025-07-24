namespace KafkaAndQueueConsumer.Service
{
    public interface IKafkaConsumer
    {
        void Consume<T>(string topic, Func<T, Task> handler, CancellationToken cancellationToken);
    }
}