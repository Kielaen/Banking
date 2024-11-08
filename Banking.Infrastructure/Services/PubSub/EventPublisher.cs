using Banking.Application.Services.PubSub;
using Dapr;
using Dapr.Client;
using Microsoft.Extensions.Logging;

namespace Banking.Infrastructure.Services.PubSub;

/// <summary>
/// Implements the <see cref="IEventPublisher"/> contract.
/// </summary>
public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly DaprClient _daprClient;
    /// <summary>
    /// Creates an instance of the <see cref="EventPublisher"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="daprClient">The Dapr client.</param>
    public EventPublisher(ILogger<EventPublisher> logger, DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    /// <inheritdoc/>
    public async Task PublishEventAsync(
        CloudEvent cloudEvent, 
        Guid CorrelationId, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogDebug("PublishEventAsync start. CorrelationId: {CorrelationId}", CorrelationId);

        await _daprClient.PublishEventAsync("pubsub", "topic", cloudEvent, cancellationToken);

        _logger.LogDebug("PublishEventAsync end. CorrelationId: {CorrelationId}", CorrelationId);
    }
}
