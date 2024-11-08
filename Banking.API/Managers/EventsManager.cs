using Banking.Application.Services.Account;
using Banking.Application.Services.PubSub;
using Dapr;

namespace Banking.API.Managers;

public class EventsManager : IEventsManager
{
    private readonly IEventPublisher _eventPublisher;
    private readonly IAccountService _accountService;
    private readonly ILogger<EventsManager> _logger;

    public EventsManager(IEventPublisher eventPublisher, IAccountService accountService, ILogger<EventsManager> logger)
    {
        _eventPublisher = eventPublisher;
        _accountService = accountService;
        _logger = logger;
    }

    public async Task ProcessEventAsync(
        CloudEvent cloudEvent, 
        Guid correlationId, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
