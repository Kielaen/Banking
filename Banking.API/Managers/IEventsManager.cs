using Banking.Domain.Models.Enums;
using Dapr;

namespace Banking.API.Managers;

/// <summary>
/// Provides a contract to manage events.
/// </summary>
public interface IEventsManager
{
    /// <summary>
    /// Processes an event and publishes the result.
    /// </summary>
    /// <param name="cloudEvent">The cloud event.</param>
    /// <param name="correlationId">The correlation identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task ProcessEventAsync<T>(
        CloudEvent<T> cloudEvent,
        Guid correlationId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Publishes an event.
    /// </summary>
    /// <typeparam name="T">The type of the event.</typeparam>
    /// <param name="numberOfEvents">The number of events to publish.</param>
    /// <param name="correlationId">The correlation identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task PublishEventAsync<T>(
    EventType eventType,
    int numberOfEvents,
    Guid correlationId,
    CancellationToken cancellationToken);
}
