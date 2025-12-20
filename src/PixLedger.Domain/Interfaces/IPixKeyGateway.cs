namespace PixLedger.Domain.Interfaces;

public interface IPixKeyGateway
{
    public Task<Guid> GetUserIdByPixKeyAsync(string key);
    public Task<string> CreateKeyAsync(string key, string kind, string userId);
}