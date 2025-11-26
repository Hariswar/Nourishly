using Confluent.Kafka;
using System.Text.Json;
using DiningAPI.Events;

namespace DiningAPI.Infrastructure;

public class KafkaConsumerService : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public KafkaConsumerService(IConfiguration configuration, ILogger<KafkaConsumerService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        
        var config = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "dining-api-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _consumer.Subscribe(configuration["Kafka:Topics:LocationEvents"]!);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);
                await ProcessMessage(result.Message.Value, result.Topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming Kafka message");
            }
        }
    }

    private async Task ProcessMessage(string message, string topic)
    {
        try
        {
            var locationEvent = JsonSerializer.Deserialize<LocationAccessedEvent>(message);
            _logger.LogInformation("Processed location event: {LocationId} - {LocationName}", 
                locationEvent?.LocationId, locationEvent?.LocationName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing location event from topic {Topic}", topic);
        }
    }

    public override void Dispose()
    {
        _consumer?.Dispose();
        base.Dispose();
    }
}