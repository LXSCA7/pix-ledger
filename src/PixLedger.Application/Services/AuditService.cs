using PixLedger.Domain.Entities;
using PixLedger.Domain.Interfaces;
using PixLedger.Domain.ValueObjects;

namespace PixLedger.Application.Services;

public class AuditService(ITransactionRepository repo)
{
    
    public async Task<bool> IsAccountCompromised(Guid accountId)
    {
        // TODO: performance Issue
        // for massive datasets (1M+ rows), loading all history into memory is not scalable.
        // future improvement: implement snapshot/Checkpoint pattern to validate only
        // transactions occurring after the last verified checkpoint.
        var transactions = (await repo.GetByAccountIdAsync(accountId)).ToList();
        if (!transactions.Any()) return false;

        var expectedPreviousHash = TransactionHash.Genesis;

        foreach (var t in transactions)
        {
            if (t.PreviousHash != expectedPreviousHash) return true;
            
            var recalculatedHash = TransactionHash.Compute(
                expectedPreviousHash, 
                t.Amount, 
                t.Id, 
                t.CreatedAt
            );
            
            if (t.CurrentHash !=  recalculatedHash) return true;
            expectedPreviousHash = recalculatedHash;
        }

        return false;
    }
}