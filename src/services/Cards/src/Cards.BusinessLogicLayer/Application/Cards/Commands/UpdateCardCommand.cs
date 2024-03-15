using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Cards.Commands;

public sealed record UpdateCardCommand(Guid Id, CardForUpdateDto CardForUpdate) : IRequest<CardDto>;

public sealed class UpdateCardHandler(IRepositoryManager repository, IMapper mapper)
    : CardHandlerBase(repository), IRequestHandler<UpdateCardCommand, CardDto>
{
    public async Task<CardDto> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {
        await CheckIfCardTypeExistsAsync(request.CardForUpdate.CardTypeId, trackChanges: false, cancellationToken);

        var card = await GetCardAndCheckIfItExistsAsync(request.Id, trackChanges: true, cancellationToken);

        mapper.Map(request.CardForUpdate, card);
        await repository.SaveAsync(cancellationToken);

        var cardDto = mapper.Map<CardDto>(card);

        return cardDto;
    }
}