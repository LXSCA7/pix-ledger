using PixLedger.Domain.Entities;

namespace PixLedger.Infrastructure.Gateways;

public class PixKeyKeyGrpcAdapter(PixService.PixServiceClient client) : IPixKeyGrpcAdapter
{
    public async Task<bool> ExistsAsync(string key, string kind)
    {
        var req = new PixRequest { Key = key, Kind = kind };

        try
        {
            var r = await client.FindKeyAsync(req);
            return r?.Exists ?? false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}