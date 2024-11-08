namespace Banking.Application.Services.Account;

/// <summary>
/// Provides a contract to interact with the account.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Allows a withdrawal operation to be performed on the account.
    /// </summary>
    /// <param name="accountId">The account identifier.</param>
    /// <param name="amount">The amount.</param>
    /// <param name="correlationId">The correlation identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A tuple containing a boolean value indicating whether the operation was successful and a message.
    /// </returns>
    Task<(bool Success, string Message)> WithdrawAsync(
        long accountId,
        decimal amount,
        Guid correlationId,
        CancellationToken cancellationToken);
}
