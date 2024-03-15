using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using Cards.BusinessLogicLayer.Entities.Exceptions.BadRequest;
using Cards.BusinessLogicLayer.Entities.Exceptions.MessagesConstants;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Repositories.Interfaces;
using Common.Logging;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Transactions.Commands;

public sealed record CreateTransactionCommand(TransactionForCreationDto TransactionForCreation) : IRequest<TransactionDto>;

public sealed class CreateTransactionHandler(IRepositoryManager repository, IMapper mapper, ILoggerManager logger) 
    : TransactionHandlerBase(repository), IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var senderCard = await GetCardAndCheckIfItExistsAsync(request.TransactionForCreation.SenderCardId, trackChanges: true, cancellationToken);
        var recipientCard = await GetCardAndCheckIfItExistsAsync(request.TransactionForCreation.RecipientCardId, trackChanges: true, cancellationToken);

        if (senderCard.Balance < request.TransactionForCreation.Amount)
        {
            throw new InsufficientFundsException(CardExceptionMessages.InsufficientFunds);
        }

        var sqlTransaction = await repository.BeginSqlTransactionAsync(cancellationToken);
        logger.LogInfo($"Starting SQL Transaction with id = {sqlTransaction.TransactionId}. SenderCardId = {senderCard.Id}, Sender balance = {senderCard.Balance}. RecipientCardId = {recipientCard.Id}, Recipient balance = {recipientCard.Balance}. Transaction amount = {request.TransactionForCreation.Amount}");

        senderCard.Balance -= request.TransactionForCreation.Amount;
        recipientCard.Balance += request.TransactionForCreation.Amount;

        var transaction = mapper.Map<Transaction>(request.TransactionForCreation);
        repository.Transaction.CreateTransaction(transaction);

        await repository.SaveAsync(cancellationToken);

        await repository.CommitSqlTransactionAsync(sqlTransaction, cancellationToken);
        logger.LogInfo($"SQL Transaction succeeded with id = {sqlTransaction.TransactionId}. Sender updated balance = {senderCard.Balance}, Recipient updated balance = {recipientCard.Balance}");

        var transactionDto = mapper.Map<TransactionDto>(transaction);

        return transactionDto;
    }
}