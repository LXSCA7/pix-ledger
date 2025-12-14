using PixLedger.Domain.Entities;
using PixLedger.Domain.Interfaces;
using PixLedger.Infrastructure.Data;

namespace PixLedger.Infrastructure.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task AddAsync(Account acc)
        => await context.AddAsync(acc);
    
    public async Task UpdateAsync(Account acc)
    {
        context.Update(acc);
        await Task.CompletedTask;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
        => await context.Accounts.FindAsync(id);
}