using Banking.Application.Services.Account;
using Microsoft.Extensions.Logging;

namespace Banking.Infrastructure.Services.Account;

/// <summary>
/// Implements the <see cref="IAccountService"/> contract.
/// </summary>
public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public AccountService(ILogger<AccountService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<(bool Success, string Message)> WithdrawAsync(
        long accountId, 
        decimal amount, 
        Guid correlationId, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogDebug("WithdrawAsync start. Account: {AccountId}. CorrelationId: {CorrelationId}", accountId, correlationId);

        decimal balance = await GetBalanceAsync(accountId);
        if (balance < amount)
        {
            return (false, "Insufficient funds");
        }

        bool updateSuccess = await UpdateBalanceAsync(accountId, amount);

        _logger.LogDebug("WithdrawAsync end. Account: {AccountId}. CorrelationId: {CorrelationId}", accountId, correlationId);

        return updateSuccess ? (true, "Withdrawal successful") : (false, "Withdrawal failed");
    }

    private static Task<decimal> GetBalanceAsync(long accountId) => Task.FromResult(1000m);
    private static Task<bool> UpdateBalanceAsync(long accountId, decimal amount) => Task.FromResult(true);
}
