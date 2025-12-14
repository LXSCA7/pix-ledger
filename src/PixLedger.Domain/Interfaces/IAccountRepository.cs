using PixLedger.Domain.Entities;

namespace PixLedger.Domain.Interfaces;

public interface IAccountRepository
{
    public Task AddAsync(Account acc);
    public Task UpdateAsync(Account acc);
    public Task<Account?> GetByIdAsync(Guid id);
}