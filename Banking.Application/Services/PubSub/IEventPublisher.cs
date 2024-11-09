using Banking.Domain.Models.Events;
using Dapr;

namespace Banking.Application.Services.PubSub;

/// <summary>
/// Provides a contract to publish events.
/// </summary>
public interface IEventPublisher
{
    Task PublishEventAsync<T>(
        T cloudEvent,
        string topic,
        Guid CorrelationId,
        CancellationToken cancellationToken);
}
