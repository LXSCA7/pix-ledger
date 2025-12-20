namespace PixLedger.Domain.Entities;

public interface IPixKeyGrpcAdapter
{
    public Task<Guid> GetUserIdByPixKeyAsync(string key);
    public Task<string> CreateKeyAsync(string key, string kind, string userId);
}