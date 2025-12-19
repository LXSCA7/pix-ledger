namespace PixLedger.Domain.Entities;

public interface IPixKeyGrpcAdapter
{
    public Task<bool> ExistsAsync(string key, string kind);
}