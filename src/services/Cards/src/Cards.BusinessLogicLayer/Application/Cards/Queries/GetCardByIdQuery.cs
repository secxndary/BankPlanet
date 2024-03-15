using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Cards.Queries;

public sealed record GetCardByIdQuery(Guid Id) : IRequest<CardDto>;

public sealed class GetCardByIdHandler(IRepositoryManager repository, IMapper mapper) : IRequestHandler<GetCardByIdQuery, CardDto>
{
    public async Task<CardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var card = await repository.Card.GetCardByIdAsync(request.Id, trackChanges: false, cancellationToken);
        var cardDto = mapper.Map<CardDto>(card);

        return cardDto;
    }
}