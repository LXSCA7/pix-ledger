namespace PixLedger.Application.DTOs;

public record TransferRequest(
    Guid SenderAccountId, 
    Guid ReceiverAccountId, 
    decimal Amount
);