using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PixLedger.Domain.Exceptions;
using PixLedger.Domain.Interfaces;
using PixLedger.Infrastructure.Data;

namespace PixLedger.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException(ex.Message, ex);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
        
    }
    public void ClearTracker()
        => context.ChangeTracker.Clear();
}