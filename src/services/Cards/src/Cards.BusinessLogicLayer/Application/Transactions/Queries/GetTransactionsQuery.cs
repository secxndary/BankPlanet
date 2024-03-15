using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Transactions.Queries;

public sealed record GetTransactionsQuery(TransactionParameters TransactionParameters) 
    : IRequest<(IEnumerable<TransactionDto> transactions, MetaData metaData)>;

public sealed class GetTransactionsHandler(IRepositoryManager repository, IMapper mapper) 
    : IRequestHandler<GetTransactionsQuery, (IEnumerable<TransactionDto> transactions, MetaData metaData)>
{
    public async Task<(IEnumerable<TransactionDto> transactions, MetaData metaData)> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactionsWithMetaData = await repository.Transaction.GetTransactionsAsync(request.TransactionParameters, trackChanges: false, cancellationToken);
        var transactionsDto = mapper.Map<IEnumerable<TransactionDto>>(transactionsWithMetaData);

        return (transactions: transactionsDto, metaData: transactionsWithMetaData.MetaData);
    }
}