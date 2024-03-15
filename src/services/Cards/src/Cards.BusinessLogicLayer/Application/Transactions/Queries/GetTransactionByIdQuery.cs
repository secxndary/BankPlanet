using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Transactions.Queries;

public sealed record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto>;

public sealed class GetTransactionByIdHandler(IRepositoryManager repository, IMapper mapper) : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await repository.Transaction.GetTransactionByIdAsync(request.Id, trackChanges: false, cancellationToken);
        var transactionDto = mapper.Map<TransactionDto>(transaction);

        return transactionDto;
    }
}