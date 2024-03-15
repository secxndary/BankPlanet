using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.CardTypes.Commands;

public sealed record CreateCardTypeCommand(CardTypeForCreationDto CardTypeForCreation) : IRequest<CardTypeDto>;

public sealed class CreateCardTypeHandler(IRepositoryManager repository, IMapper mapper) : IRequestHandler<CreateCardTypeCommand, CardTypeDto>
{
    public async Task<CardTypeDto> Handle(CreateCardTypeCommand request, CancellationToken cancellationToken)
    {
        var cardType = mapper.Map<CardType>(request.CardTypeForCreation);

        repository.CardType.CreateCardType(cardType);
        await repository.SaveAsync(cancellationToken);

        var cardTypeDto = mapper.Map<CardTypeDto>(cardType);

        return cardTypeDto;
    }
}