
using Confluent.Kafka;
using System.Text.Json;

namespace KafkaAndQueueConsumer.Service
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IConsumer<Null, string> _consumer;

        public KafkaConsumer(IConsumer<Null, string> consumer)
        {
            _consumer = consumer;
        }

        public void Consume<T>(string topic, Func<T, Task> handler, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(cancellationToken);
                    var message = JsonSerializer.Deserialize<T>(result.Message.Value);
                    handler(message!);
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }
    }
}
