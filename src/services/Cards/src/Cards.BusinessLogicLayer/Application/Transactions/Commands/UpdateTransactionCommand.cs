using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Transactions.Commands;

public sealed record UpdateTransactionCommand(Guid Id, TransactionForUpdateDto TransactionForUpdate) : IRequest<TransactionDto>;

public sealed class UpdateTransactionHandler(IRepositoryManager repository, IMapper mapper)
    : TransactionHandlerBase(repository), IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    public async Task<TransactionDto> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        await CheckIfCardExistsAsync(request.TransactionForUpdate.SenderCardId, trackChanges: false, cancellationToken);
        await CheckIfCardExistsAsync(request.TransactionForUpdate.RecipientCardId, trackChanges: false, cancellationToken);

        var transaction = await GetTransactionAndCheckIfItExistsAsync(request.Id, trackChanges: true, cancellationToken);

        mapper.Map(request.TransactionForUpdate, transaction);
        await repository.SaveAsync(cancellationToken);

        var transactionDto = mapper.Map<TransactionDto>(transaction);

        return transactionDto;
    }
}