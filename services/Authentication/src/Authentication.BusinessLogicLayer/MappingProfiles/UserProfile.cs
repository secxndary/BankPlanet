using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.DataAccessLayer.Entities.Models;
using AutoMapper;

namespace Authentication.BusinessLogicLayer.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile() 
    {
        CreateMap<UserForRegistrationDto, User>().ReverseMap();

        CreateMap<UserDto, User>().ReverseMap();
    }
}