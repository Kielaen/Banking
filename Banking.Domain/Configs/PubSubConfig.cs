namespace Banking.Domain.Configs;

/// <summary>
/// Decribes the configuration for the pub/sub topics.
/// </summary>
public class PubSubConfig
{
    /// <summary>
    /// The topic for all withdrawals.
    /// </summary>
    public const string WithdrawalTopic = "ALL_WITHDRAWALS";

    /// <summary>
    /// The topic for all withdrawal responses.
    /// </summary>
    public const string WithdrawalResponseTopic = "ALL_WITHDRAWAL_RESPONSE";
}
