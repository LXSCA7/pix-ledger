namespace PixLedger.Application.DTOs;

public record TransferRequest(
    Guid SenderAccountId, 
    string ReceiverPixKey, 
    decimal Amount
);