using Microsoft.AspNetCore.Mvc;
using PixLedger.Application.DTOs;
using PixLedger.Application.Services;

namespace PixLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController(TransactionService transactionSvc) : ControllerBase
{

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer(TransferRequest request)
    {
        try
        {
            var transferId = await transactionSvc.TransferAsync(request);
            return Ok(new {transactionId = transferId});
        }
        catch (Exception ex)
        {
            // momentaneo, na app real faria um middleware pra try-catch, acho mais elegante
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{key}")]
    public async Task<IActionResult> PixTest(string key)
    {
        return Ok(new { exists = transactionSvc.FindPixKey(key) });
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit(DepositRequest request)
    {
        try
        {
            await transactionSvc.DepositAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}