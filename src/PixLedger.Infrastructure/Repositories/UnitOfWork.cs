using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PixLedger.Domain.Interfaces;
using PixLedger.Infrastructure.Data;

namespace PixLedger.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
        => await context.SaveChangesAsync();
    public void ClearTracker()
        => context.ChangeTracker.Clear();
}