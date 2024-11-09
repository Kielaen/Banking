using System;
using System.Text.Json.Serialization;

namespace Banking.Domain.Models.Events;

/// <summary>
/// The withdrawal event.
/// </summary>
public class WithdrawalEvent
{
    private static Random _random = new Random();

    /// <summary>
    /// The amount to withdraw.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// The account identifier.
    /// </summary>
    [JsonPropertyName("accountId")]
    public long AccountId { get; set; }

    public static WithdrawalEvent GenerateRandomWithdrawalEvent()
    {
        return new WithdrawalEvent
        {
            Amount = _random.Next(100, 10000) / 100m, 
            AccountId = _random.Next(100000, 999999)
        };
    }
}
