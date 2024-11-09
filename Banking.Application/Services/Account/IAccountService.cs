using Banking.Domain.Models.Events;

namespace Banking.Application.Services.Account;

/// <summary>
/// Provides a contract to interact with the account.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Allows a withdrawal operation to be performed on the account.
    /// </summary>
    /// <param name="withdrawal">The withdrawal.</param>
    /// <param name="correlationId">The correlation identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A response of the withdrawal as a <see cref="WithdrawalResponse"/>.
    /// </returns>
    Task<WithdrawalResponse> WithdrawAsync(
        WithdrawalEvent withdrawal,
        Guid correlationId,
        CancellationToken cancellationToken);
}
