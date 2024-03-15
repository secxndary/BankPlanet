using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;
using Cards.DataAccessLayer.Entities.Models;

namespace Cards.BusinessLogicLayer.MappingProfiles;

public class CardTypeProfile : Profile
{
    public CardTypeProfile()
    {
        CreateMap<CardType, CardTypeDto>();

        CreateMap<CardTypeForCreationDto, CardType>();

        CreateMap<CardTypeForUpdateDto, CardType>().ReverseMap();
    }
}