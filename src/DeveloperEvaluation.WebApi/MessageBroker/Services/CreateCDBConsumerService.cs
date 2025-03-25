using System.Text;
using System.Text.Json;
using DeveloperEvaluation.Application.CDBs.CreateCDB;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DeveloperEvaluation.WebApi.MessageBroker.Services
{
    public class CreateCDBConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CreateCDBConsumerService>? _logger;
        private readonly RabbitMQSetting? _rabbitMqSetting;
        private readonly IConnection? _connection;
        private readonly IChannel? _channel;
        private readonly IMediator? _mediator;
        private readonly IServiceScope? _scope;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public CreateCDBConsumerService(IOptions<RabbitMQSetting> rabbitMqSetting, IServiceProvider serviceProvider, ILogger<CreateCDBConsumerService> logger)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
            _serviceProvider = serviceProvider;
            _logger = logger;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting?.HostName ?? string.Empty,
                UserName = _rabbitMqSetting?.UserName ?? string.Empty,
                Password = _rabbitMqSetting?.Password ?? string.Empty
            };
            _connection = factory?.CreateConnectionAsync().Result;
            _channel = _connection?.CreateChannelAsync().Result;
            _scope = _serviceProvider.CreateScope();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartConsuming(RabbitMQQueues.CreateCDBQueue, stoppingToken);
            await Task.CompletedTask;
        }

        async Task StartConsuming(string queueName, CancellationToken cancellationToken)
        {
            if (_channel == null)
                return;

            var queueDeclareOk = await _channel.QueueDeclareAsync(queueName ?? string.Empty, false, false, false, null, cancellationToken: cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                bool processedSuccessfully = false;
                try
                {
                    processedSuccessfully = await ProcessMessageAsync(message, cancellationToken);
                }
                catch
                {
                    _logger?.LogError("Exception occurred while processing message from queue");
                }

                if (processedSuccessfully)
                {
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                else
                {
                    await _channel.BasicRejectAsync(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            };

            await _channel.BasicConsumeAsync(queueName ?? string.Empty, false, consumer, cancellationToken: cancellationToken);
        }

        async Task<bool> ProcessMessageAsync(string message, CancellationToken cancellationToken)
        {
            try
            {
                var createCDBCommand = JsonSerializer.Deserialize<CreateCDBCommand>(message, _jsonOptions);

                if (createCDBCommand == null)
                {
                    _logger?.LogError("Failed to deserialize message to CreateCDBCommand.");
                    return false;
                }

                if (_mediator == null)
                {
                    _logger?.LogError("Failed to send message from mediator.");
                    return false;
                }

                var createCDBResult = await _mediator.Send(createCDBCommand, cancellationToken);

                _logger?.LogInformation("Message processed successfully");

                return true;
            }
            catch
            {
                _logger?.LogError("Error processing message");
                return false;
            }
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            _scope?.Dispose();

            GC.SuppressFinalize(this);

            base.Dispose();
        }
    }
}
