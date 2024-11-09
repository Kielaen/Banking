using Banking.Application.Services.Account;
using Banking.Domain.Configs;
using Banking.Domain.Models.Events;
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
    public async Task<WithdrawalResponse> WithdrawAsync(
        WithdrawalEvent withdrawal,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogDebug("WithdrawAsync start. Account: {AccountId}. CorrelationId: {CorrelationId}", withdrawal.AccountId, correlationId);

        var balance = await GetBalanceAsync(withdrawal.AccountId);
        if (balance < withdrawal.Amount)
        {
            return WithdrawalResponse.CreateWithdrawalResponse(withdrawal, balance, false, MessageConstants.InsufficientFunds);
        }

        var withdrawalResponse = UpdateBalanceAsync(withdrawal, balance);

        _logger.LogDebug("WithdrawAsync end. Account: {AccountId}. CorrelationId: {CorrelationId}", withdrawal.AccountId, correlationId);

        return withdrawalResponse;
    }

    private static Task<decimal> GetBalanceAsync(long accountId) => Task.FromResult(1000m);
    private static WithdrawalResponse UpdateBalanceAsync(WithdrawalEvent withdrawal, decimal balance)
    {
        try
        {
            var remainingBalance = balance - withdrawal.Amount;
            return WithdrawalResponse.CreateWithdrawalResponse(withdrawal, remainingBalance, true, MessageConstants.WithdrawalSuccessful);
        }
        catch
        {
            return WithdrawalResponse.CreateWithdrawalResponse(withdrawal, balance, false, MessageConstants.WithdrawalFailed);
        }
    }
}
