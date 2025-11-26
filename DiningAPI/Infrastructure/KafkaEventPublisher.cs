using Confluent.Kafka;
using System.Text.Json;
using DiningAPI.Events;

namespace DiningAPI.Infrastructure;

public class KafkaEventPublisher : IEventPublisher, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly IConfiguration _configuration;
    private readonly ILogger<KafkaEventPublisher> _logger;

    public KafkaEventPublisher(IConfiguration configuration, ILogger<KafkaEventPublisher> logger)
    {
        _configuration = configuration;
        _logger = logger;
        
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync<T>(T domainEvent) where T : IDomainEvent
    {
        if (domainEvent is not LocationAccessedEvent)
            return; // Only handle location events
            
        var topic = _configuration["Kafka:Topics:LocationEvents"];

        try
        {
            var json = JsonSerializer.Serialize(domainEvent);
            var message = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = json
            };

            await _producer.ProduceAsync(topic!, message);
            _logger.LogInformation("Event {EventType} sent to Kafka topic {Topic}", typeof(T).Name, topic);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending event {EventType} to Kafka topic {Topic}", typeof(T).Name, topic);
        }
    }

    public void Dispose()
    {
        _producer?.Dispose();
    }
}