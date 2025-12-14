using System.Runtime.Serialization;
using PixLedger.Domain.ValueObjects;

namespace PixLedger.Domain.Entities;

public class Account : AuditableEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public decimal Balance { get; private set; }
    public TransactionHash LastTransactionHash { get; private set; }
    public int Version { get; private set; }
    
    [IgnoreDataMember]
    public string FullName { get => $"{FirstName} {LastName}"; }

    private Account() { }

    public Account(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Balance = 0;
        LastTransactionHash = TransactionHash.Genesis;
    }
    
    public void ApplyTransaction(Transaction transaction)
    {
        if (transaction.AccountId != Id) throw new InvalidOperationException("transaction does not belong to this account");
        if (transaction.PreviousHash != LastTransactionHash) throw new InvalidOperationException("integrity violation: invalid previous hash");
        if (this.Balance + transaction.Amount < 0) 
            throw new InvalidOperationException("Insufficient balance");
        
        LastTransactionHash = transaction.CurrentHash;
        Balance += transaction.Amount;
        
        Update();
    }
}