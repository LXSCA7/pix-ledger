namespace PixLedger.Application.DTOs;

public record DepositRequest(Guid AccountId, decimal Amount);