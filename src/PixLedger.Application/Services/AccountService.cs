using PixLedger.Application.DTOs;
using PixLedger.Domain.Entities;
using PixLedger.Domain.Interfaces;

namespace PixLedger.Application.Services;

public class AccountService(IAccountRepository repo, IUnitOfWork unitOfWork)
{
    public async Task<Account> CreateAccount(CreateAccountRequest request)
    {
        var acc = new Account(request.FirstName, request.LastName);
        await repo.AddAsync(acc);
        await unitOfWork.CommitAsync();
        return acc;
    }
}