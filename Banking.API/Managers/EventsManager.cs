using Banking.Application.Services.Account;
using Banking.Application.Services.PubSub;
using Banking.Domain.Configs;
using Banking.Domain.Models.Enums;
using Banking.Domain.Models.Events;
using Dapr;

namespace Banking.API.Managers;

public class EventsManager : IEventsManager
{
    private readonly IEventPublisher _eventPublisher;
    private readonly IAccountService _accountService;
    private readonly ILogger<EventsManager> _logger;

    public EventsManager(
        IEventPublisher eventPublisher,
        IAccountService accountService,
        ILogger<EventsManager> logger)
    {
        _eventPublisher = eventPublisher;
        _accountService = accountService;
        _logger = logger;
    }

    public async Task ProcessEventAsync<T>(
        CloudEvent<T> cloudEvent,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogDebug("ProcessEventAsync start. CorrelationId: {CorrelationId}", correlationId);

        if (cloudEvent.Data is WithdrawalEvent withdrawalEvent)
        {
            await ProcessWithdrawalEventAsync(withdrawalEvent, correlationId, cancellationToken);
        }

        _logger.LogDebug("ProcessEventAsync end. CorrelationId: {CorrelationId}", correlationId);
    }

    public async Task PublishEventAsync<T>(
        EventType eventType,
        int numberOfEvents,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        if (eventType is EventType.Withdrawal)
        {
            for (var i = 0; i < numberOfEvents; i++)
            {
                await _eventPublisher.PublishEventAsync(
                    WithdrawalEvent.GenerateRandomWithdrawalEvent(),
                    PubSubConfig.WithdrawalTopic,
                    correlationId,
                    cancellationToken);
            }
        }
    }

    private async Task ProcessWithdrawalEventAsync(
        WithdrawalEvent withdrawalEvent,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogDebug("ProcessWithdrawalEventAsync start. CorrelationId: {CorrelationId}", correlationId);

        var account = await _accountService.WithdrawAsync(withdrawalEvent, correlationId, cancellationToken);

        await _eventPublisher.PublishEventAsync(account, PubSubConfig.WithdrawalResponseTopic, correlationId, cancellationToken);

        _logger.LogDebug("ProcessWithdrawalEventAsync end. CorrelationId: {CorrelationId}", correlationId);
    }
}
