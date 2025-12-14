using PixLedger.Application.DTOs;
using PixLedger.Domain.Entities;
using PixLedger.Domain.Interfaces;

namespace PixLedger.Application.Services;

public class TransactionService(ITransactionRepository transactionRepo, IAccountRepository accountRepo, IUnitOfWork unitOfWork)
{
    public async Task<Guid> TransferAsync(TransferRequest transferRequest)
    {
        var senderAcc = await accountRepo.GetByIdAsync(transferRequest.SenderAccountId) 
                        ?? throw new Exception("sender account not found. [todo notfound ex]");
        
        var receiverAcc = await accountRepo.GetByIdAsync(transferRequest.ReceiverAccountId) 
                          ?? throw new Exception("receiver account not found. [todo notfound ex]");

        var correlationId = Guid.CreateVersion7();
        
        var senderTransaction = new Transaction(
            transferRequest.SenderAccountId, 
            transferRequest.Amount, 
            TransactionType.Debit, 
            correlationId, 
            senderAcc.LastTransactionHash
        );
        
        var receiverTransaction = new Transaction(
            transferRequest.ReceiverAccountId, 
            transferRequest.Amount, 
            TransactionType.Credit, 
            correlationId, 
            receiverAcc.LastTransactionHash
        );
        
        senderAcc.ApplyTransaction(senderTransaction);
        receiverAcc.ApplyTransaction(receiverTransaction);
        
        await transactionRepo.AddAsync(senderTransaction);
        await transactionRepo.AddAsync(receiverTransaction);
        await accountRepo.UpdateAsync(senderAcc);
        await accountRepo.UpdateAsync(receiverAcc);
        
        await unitOfWork.CommitAsync();

        return correlationId;
    }

    public async Task DepositAsync(DepositRequest request)
    {
        if (request.Amount <= 0) throw new Exception("Amount must be greater than zero");
        var acc = await accountRepo.GetByIdAsync(request.AccountId);
        if (acc == null) throw new Exception("Account not found");

        var correlationId = Guid.CreateVersion7();
        var transaction = new Transaction(acc.Id, request.Amount, TransactionType.Credit, correlationId, acc.LastTransactionHash);
        acc.ApplyTransaction(transaction);
        
        await transactionRepo.AddAsync(transaction);
        await unitOfWork.CommitAsync();
    }
}