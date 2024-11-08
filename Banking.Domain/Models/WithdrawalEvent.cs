namespace Banking.Domain.Models;

public record WithdrawalEvent(decimal Amount, long AccountId, string Status);
