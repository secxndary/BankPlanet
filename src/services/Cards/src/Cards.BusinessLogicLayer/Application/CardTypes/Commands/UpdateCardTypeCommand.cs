using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.CardTypes.Commands;

public sealed record UpdateCardTypeCommand(Guid Id, CardTypeForUpdateDto CardTypeForUpdate) : IRequest<CardTypeDto>;

public sealed class UpdateCardTypeHandler(IRepositoryManager repository, IMapper mapper)
    : CardTypeHandlerBase(repository), IRequestHandler<UpdateCardTypeCommand, CardTypeDto>
{
    public async Task<CardTypeDto> Handle(UpdateCardTypeCommand request, CancellationToken cancellationToken)
    {
        var cardType = await GetCardTypeAndCheckIfItExistsAsync(request.Id, trackChanges: true, cancellationToken);

        mapper.Map(request.CardTypeForUpdate, cardType);
        await repository.SaveAsync(cancellationToken);

        var cardTypeDto = mapper.Map<CardTypeDto>(cardType);

        return cardTypeDto;
    }
}