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
    Task ProcessEventAsync(
        CloudEvent cloudEvent,
        Guid correlationId,
        CancellationToken cancellationToken);
}
