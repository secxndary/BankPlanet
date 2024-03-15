using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;
using Cards.DataAccessLayer.Entities.Models;

namespace Cards.BusinessLogicLayer.MappingProfiles;

public class CardProfile : Profile
{
    public CardProfile()
    {
        CreateMap<Card, CardDto>();

        CreateMap<CardForCreationDto, Card>();

        CreateMap<CardForUpdateDto, Card>().ReverseMap();
    }
}