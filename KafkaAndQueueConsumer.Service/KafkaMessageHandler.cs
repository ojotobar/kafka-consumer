using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace KafkaAndQueueConsumer.Service
{
    public class KafkaMessageHandler : IKafkaMessageHandler
    {
        private readonly ILogger<KafkaMessageHandler> _logger;

        public KafkaMessageHandler(ILogger<KafkaMessageHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(Person message)
        {
            _logger.LogInformation($"Message received: {JsonSerializer.Serialize(message)}");
            // Save to database? Send an email? Call another service?
            await Task.CompletedTask;
        }
    }
}
