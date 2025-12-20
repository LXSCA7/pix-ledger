using Microsoft.AspNetCore.Mvc;
using PixLedger.Application.DTOs;
using PixLedger.Application.Services;

namespace PixLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class PixController(PixKeyAppService pixKeyAppSvc) : ControllerBase
{
    [HttpGet("{key}")]
    public async Task<IActionResult> PixGet(string key)
    {
        return Ok(new { userId = await pixKeyAppSvc.GetUserIdByPixKeyAsync(key) });
    }
    
    [HttpPost]
    public async Task<IActionResult> PixCreate(CreatePixKeyRequest req)
    {
        return Ok(new { key = await pixKeyAppSvc.CreatePixKeyAsync(req) });
    }
    
}