using PixLedger.Application.DTOs;
using PixLedger.Domain.Interfaces;

namespace PixLedger.Application.Services;

public class PixKeyAppService(IPixKeyGateway pixKeyGateway)
{
    public async Task<Guid> GetUserIdByPixKeyAsync(string key)
    {
        return await pixKeyGateway.GetUserIdByPixKeyAsync(key);
    }
    
    public async Task<string> CreatePixKeyAsync(CreatePixKeyRequest req)
    {
        return await pixKeyGateway.CreateKeyAsync(req.Key, req.Kind, req.UserId);
    }
}