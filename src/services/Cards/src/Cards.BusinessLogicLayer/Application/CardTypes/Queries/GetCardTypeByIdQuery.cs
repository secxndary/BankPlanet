using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.CardTypes.Queries;

public sealed record GetCardTypeByIdQuery(Guid Id) : IRequest<CardTypeDto>;

public sealed class GetCardTypeByIdHandler(IRepositoryManager repository, IMapper mapper) : IRequestHandler<GetCardTypeByIdQuery, CardTypeDto>
{
    public async Task<CardTypeDto> Handle(GetCardTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var cardType = await repository.CardType.GetCardTypeByIdAsync(request.Id, trackChanges: false, cancellationToken);
        var cardTypeDto = mapper.Map<CardTypeDto>(cardType);

        return cardTypeDto;
    }
}