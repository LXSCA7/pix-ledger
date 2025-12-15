using Microsoft.AspNetCore.Mvc;
using PixLedger.Application.Services;

namespace PixLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditController(AuditService auditService) : ControllerBase
{
    [HttpGet("{accountId}")]
    public async Task<IActionResult> Audit(Guid accountId)
    {
        var result = await auditService.IsAccountCompromised(accountId);
        return Ok(new {isAccountCompromised = result});
    }
}