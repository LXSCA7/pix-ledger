using PixLedger.Domain.Entities;

namespace PixLedger.Infrastructure.Gateways;

public class PixKeyGateway(PixService.PixServiceClient client) : IPixKeyGateway
{
    public async Task<Guid> GetUserIdByPixKeyAsync(string key)
    {
        var req = new PixRequest { Key = key };
        var r = await client.FindKeyAsync(req);
        if (r == null || string.IsNullOrEmpty(r.UserId))
        {
            // validate later
            return Guid.Empty;
        }
        
        return Guid.Parse(r.UserId);
    }

    public async Task<string> CreateKeyAsync(string key, string kind, string userId)
    {
        var req = new PixCreateKeyRequest { Key = key, Kind = kind, UserId = userId };
        var r = await client.CreateKeyAsync(req);
        return r.Key;
    }
}