using PixLedger.Domain.ValueObjects;

namespace PixLedger.Domain.Entities;

public enum TransactionType 
{
    Credit = 1,
    Debit = 2,
}

public class Transaction : Entity
{
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid CorrelationId  { get; private set; }
    public TransactionHash PreviousHash { get; private set; }
    public TransactionHash CurrentHash { get; private set; }
    
    public Transaction(Guid accountId, 
        decimal amount, 
        TransactionType type, 
        Guid correlationId, 
        TransactionHash previousHash)
    {
        AccountId = accountId;
        Amount = amount;
        Type = type;
        CorrelationId = correlationId;
        PreviousHash = previousHash;

        if (amount <= 0) throw new Exception("[todo: domainexception]");
        if (type == TransactionType.Debit) Amount = -amount;
        
        CurrentHash = TransactionHash.Compute(previousHash, Amount, Id, CreatedAt);
    }
}