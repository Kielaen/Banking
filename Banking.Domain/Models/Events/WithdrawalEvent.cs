namespace Banking.Domain.Models.Events;

public record WithdrawalEvent(decimal Amount, long AccountId, string Status);
