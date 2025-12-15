using PixLedger.Domain.Entities;

namespace PixLedger.Domain.Interfaces;

public interface ITransactionRepository
{
    public Task AddAsync(Transaction transaction);
    public Task<Transaction?> GetByIdAsync(Guid id);
    public Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId);
}