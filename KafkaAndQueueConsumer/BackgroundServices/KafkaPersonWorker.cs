using KafkaAndQueueConsumer.Service;

namespace KafkaAndQueueConsumer.BackgroundServices
{
    public class KafkaPersonWorker : BackgroundService
    {
        private readonly IKafkaConsumer _consumer;
        private readonly IKafkaMessageHandler _handler;

        public KafkaPersonWorker(IKafkaConsumer consumer, IKafkaMessageHandler handler)
        {
            _consumer = consumer;
            _handler = handler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _consumer.Consume<Person>($"{nameof(Person)}", async msg =>
                {
                    await _handler.HandleAsync(msg);
                }, CancellationToken.None);

                await Task.CompletedTask;
            }
        }
    }
}
