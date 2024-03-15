using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Cards.Commands;

public sealed record DeleteCardCommand(Guid Id) : IRequest;

public sealed class DeleteCardHandler(IRepositoryManager repository) 
    : CardHandlerBase(repository), IRequestHandler<DeleteCardCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        var card = await GetCardAndCheckIfItExistsAsync(request.Id, trackChanges: false, cancellationToken);

        repository.Card.DeleteCard(card);
        await repository.SaveAsync(cancellationToken);

        return Unit.Value;
    }
}