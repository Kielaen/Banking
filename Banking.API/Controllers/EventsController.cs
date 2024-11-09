using Banking.API.Managers;
using Banking.Domain.Configs;
using Banking.Domain.Models.Enums;
using Banking.Domain.Models.Events;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[Route("events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventsManager _eventsManager;
    private readonly ILogger<EventsController> _logger;

    /// <summary>
    /// Creates an instance of the <see cref="EventsController"/> class.
    /// </summary>
    /// <param name="eventsManager">The events manager.</param>
    /// <param name="logger">The logger.</param>
    public EventsController(IEventsManager eventsManager, ILogger<EventsController> logger)
    {
        _eventsManager = eventsManager;
        _logger = logger;
    }

    /// <summary>
    /// Process Event 
    /// </summary>
    /// <param name="cloudEvent">The event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    [Topic(DaprComponents.WithdrawalPubSub, PubSubConfig.WithdrawalTopic)]
    [HttpPost("withdrawals/subscribe")]
    public async Task<IActionResult> ProcessWithdrawalEventAsync([FromBody] CloudEvent<WithdrawalEvent> cloudEvent, CancellationToken cancellationToken)
    {
        Guid correlationId = Guid.NewGuid();
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("ProcessWithdrawalEventAsync start. CorrelationId: {CorrelationId}", correlationId);

        await _eventsManager.ProcessEventAsync(cloudEvent, correlationId, cancellationToken);

        _logger.LogInformation("ProcessWithdrawalEventAsync end. CorrelationId: {CorrelationId}", correlationId);

        return Ok();
    }

    [HttpGet("withdrawals/publish/numberOfEvents/{numberOfEvents}")]
    public async Task<IActionResult> PublishWithdrawalEventAsync([FromRoute] int numberOfEvents, CancellationToken cancellationToken)
    {
        Guid correlationId = Guid.NewGuid();
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("PublishWithdrawalEventAsync start. CorrelationId: {CorrelationId}", correlationId);

        await _eventsManager.PublishEventAsync<WithdrawalEvent>(EventType.Withdrawal, numberOfEvents, correlationId, cancellationToken);

        _logger.LogInformation("PublishWithdrawalEventAsync end. CorrelationId: {CorrelationId}", correlationId);

        return Ok();
    }

}
