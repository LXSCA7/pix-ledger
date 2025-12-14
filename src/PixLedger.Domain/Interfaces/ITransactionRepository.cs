using PixLedger.Domain.Entities;

namespace PixLedger.Domain.Interfaces;

public interface ITransactionRepository
{
    public Task AddAsync(Transaction transaction);
    public Task<Transaction?> GetByIdAsync(Guid id);
}