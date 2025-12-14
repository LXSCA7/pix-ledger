using System.Security.Cryptography;
using System.Text;

namespace PixLedger.Domain.ValueObjects;

public record TransactionHash
{
    public static readonly TransactionHash Genesis = new(new string('0', 64));
    public string Value { get; }
    
    private TransactionHash(string value) 
    {
        if (string.IsNullOrEmpty(value) || value.Length != 64)
            throw new Exception("to-do: domainexception");
        Value = value;
    }

    public static TransactionHash Compute(TransactionHash? previous, decimal amount, Guid transactionId, DateTime createdAt)
    {
        var parent = previous ?? Genesis;
        var payload = $"{parent}-{amount:F2}-{transactionId}-{createdAt:O}";
        
        var bytes = Encoding.UTF8.GetBytes(payload);
        var hashBytes = SHA256.HashData(bytes);
        var hashString = Convert.ToHexStringLower(hashBytes);

        return new(hashString);
    }

    public static TransactionHash Load(string value) => new(value);
    
    public override string ToString() => Value;
}