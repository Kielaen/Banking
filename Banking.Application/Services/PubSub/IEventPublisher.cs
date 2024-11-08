using Dapr;

namespace Banking.Application.Services.PubSub;

/// <summary>
/// Provides a contract to publish events.
/// </summary>
public interface IEventPublisher
{
    Task PublishEventAsync(
        CloudEvent cloudEvent,
        Guid CorrelationId,
        CancellationToken cancellationToken);
}
