using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Cards.DataAccessLayer.Repositories.Interfaces;
using MediatR;

namespace Cards.BusinessLogicLayer.Application.CardTypes.Queries;

public sealed record GetCardTypesQuery(CardTypeParameters CardTypeParameters) 
    : IRequest<(IEnumerable<CardTypeDto> cardTypes, MetaData metaData)>;

public sealed class GetCardTypesHandler(IRepositoryManager repository, IMapper mapper) 
    : IRequestHandler<GetCardTypesQuery, (IEnumerable<CardTypeDto> cardTypes, MetaData metaData)>
{
    public async Task<(IEnumerable<CardTypeDto> cardTypes, MetaData metaData)> Handle(GetCardTypesQuery request, CancellationToken cancellationToken)
    {
        var cardTypesWithMetaData = await repository.CardType.GetCardTypesAsync(request.CardTypeParameters, trackChanges: false, cancellationToken);
        var cardTypesDto = mapper.Map<IEnumerable<CardTypeDto>>(cardTypesWithMetaData);

        return (cardTypes: cardTypesDto, metaData: cardTypesWithMetaData.MetaData);
    }
}