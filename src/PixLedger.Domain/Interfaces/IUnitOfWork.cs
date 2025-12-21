namespace PixLedger.Domain.Interfaces;

public interface IUnitOfWork
{
    public Task CommitAsync();
    void ClearTracker();
}