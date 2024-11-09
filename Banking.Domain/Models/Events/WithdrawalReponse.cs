namespace Banking.Domain.Models.Events;

/// <summary>
/// The withdrawal response after a withdrawal operation.
/// </summary>
public class WithdrawalResponse
{
    /// <summary>
    /// Gets or sets the account identifier.
    /// </summary>
    public long AccountId { get; set; }

    /// <summary>
    /// The amount withdrawn.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The balance after the withdrawal.
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public string Message { get; set; } = default!;

    public static WithdrawalResponse CreateWithdrawalResponse(
        WithdrawalEvent withdrawal,
        decimal balance,
        bool success,
        string message) => new()
        {
            AccountId = withdrawal.AccountId,
            Amount = withdrawal.Amount,
            Balance = balance,
            Success = success,
            Message = message
        };
}
