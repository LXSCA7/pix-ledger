using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PixLedger.Application.DTOs;
using PixLedger.Application.Services;
using PixLedger.Domain.Entities;

namespace PixLedger.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController(AccountService accountSvc) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        try
        {
            var acc = await accountSvc.CreateAccount(request);
            return CreatedAtAction(nameof(GetAccountById), new { account = acc.Id }, acc);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountById(Guid accountId)
    {
        return Ok();
    }
}