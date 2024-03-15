using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.CardTypes.Commands;

public sealed record DeleteCardTypeCommand(Guid Id) : IRequest;

public sealed class DeleteCardTypeHandler(IRepositoryManager repository) 
    : CardTypeHandlerBase(repository), IRequestHandler<DeleteCardTypeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCardTypeCommand request, CancellationToken cancellationToken)
    {
        var cardType = await GetCardTypeAndCheckIfItExistsAsync(request.Id, trackChanges: false, cancellationToken);

        repository.CardType.DeleteCardType(cardType);
        await repository.SaveAsync(cancellationToken);

        return Unit.Value;
    }
}