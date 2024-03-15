using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.Cards.Queries;

public sealed record GetCardsQuery(CardParameters CardParameters) 
    : IRequest<(IEnumerable<CardDto> cards, MetaData metaData)>;

public sealed class GetCardsHandler(IRepositoryManager repository, IMapper mapper) 
    : IRequestHandler<GetCardsQuery, (IEnumerable<CardDto> cards, MetaData metaData)>
{
    public async Task<(IEnumerable<CardDto> cards, MetaData metaData)> Handle(GetCardsQuery request, CancellationToken cancellationToken)
    {
        var cardsWithMetaData = await repository.Card.GetCardsAsync(request.CardParameters, trackChanges: false, cancellationToken);
        var cardsDto = mapper.Map<IEnumerable<CardDto>>(cardsWithMetaData);

        return (cards: cardsDto, metaData: cardsWithMetaData.MetaData);
    }
}