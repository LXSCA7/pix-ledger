using PixLedger.Domain.Interfaces;
using PixLedger.Infrastructure.Data;

namespace PixLedger.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
        => await context.SaveChangesAsync();
}