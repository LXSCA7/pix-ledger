using Microsoft.EntityFrameworkCore;
using PixLedger.Domain.Entities;
using PixLedger.Domain.Interfaces;
using PixLedger.Infrastructure.Data;

namespace PixLedger.Infrastructure.Repositories;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    public async Task AddAsync(Transaction acc)
        => await context.AddAsync(acc);
    
    public async Task UpdateAsync(Transaction acc)
    {
        context.Update(acc);
        await Task.CompletedTask;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
        => await context.Transactions.FindAsync(id);

    public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId)
        => await context.Transactions.Where(t => t.AccountId == accountId)
                                     .OrderBy(t => t.Id)
                                     .AsNoTracking()
                                     .ToListAsync();
}