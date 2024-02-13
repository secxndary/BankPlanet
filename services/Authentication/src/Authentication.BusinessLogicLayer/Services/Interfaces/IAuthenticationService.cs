using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.DataAccessLayer.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.BusinessLogicLayer.Services.Interfaces;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication);
    Task<TokenDto> CreateToken(bool populateExpiration);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
    Task<UserDto> GetUserByToken(TokenDto tokenDto);
    Task<IdentityUser?> GetUserById(string id);
    User? GetCurrentUser();
}