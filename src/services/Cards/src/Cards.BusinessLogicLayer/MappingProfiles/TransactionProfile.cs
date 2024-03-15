using AutoMapper;
using Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;
using Cards.DataAccessLayer.Entities.Models;

namespace Cards.BusinessLogicLayer.MappingProfiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDto>();

        CreateMap<TransactionForCreationDto, Transaction>();

        CreateMap<TransactionForUpdateDto, Transaction>().ReverseMap();
    }
}