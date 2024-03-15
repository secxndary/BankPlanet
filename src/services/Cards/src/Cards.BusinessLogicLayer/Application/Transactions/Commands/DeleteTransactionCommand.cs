using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Transactions.Commands;

public sealed record DeleteTransactionCommand(Guid Id) : IRequest;

public sealed class DeleteTransactionHandler(IRepositoryManager repository) 
    : TransactionHandlerBase(repository), IRequestHandler<DeleteTransactionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await GetTransactionAndCheckIfItExistsAsync(request.Id, trackChanges: false, cancellationToken);

        repository.Transaction.DeleteTransaction(transaction);
        await repository.SaveAsync(cancellationToken);

        return Unit.Value;
    }
}