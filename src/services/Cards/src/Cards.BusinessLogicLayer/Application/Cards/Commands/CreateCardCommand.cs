using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Cards.Commands;

public sealed record CreateCardCommand(CardForCreationDto CardForCreation) : IRequest<CardDto>;

public sealed class CreateCardHandler(IRepositoryManager repository, IMapper mapper) 
    : CardHandlerBase(repository), IRequestHandler<CreateCardCommand, CardDto>
{
    public async Task<CardDto> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        await CheckIfCardTypeExistsAsync(request.CardForCreation.CardTypeId, trackChanges: false, cancellationToken);

        var card = mapper.Map<Card>(request.CardForCreation);

        repository.Card.CreateCard(card);
        await repository.SaveAsync(cancellationToken);

        var cardDto = mapper.Map<CardDto>(card);

        return cardDto;
    }
}