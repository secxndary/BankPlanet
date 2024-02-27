using Authentication.BusinessLogicLayer.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Authentication.BusinessLogicLayer.Services.Interfaces;

public interface IAuthenticationService
{
    Task RegisterUserAsync(UserForRegistrationDto userForRegistration, CancellationToken cancellationToken);
    Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication, CancellationToken cancellationToken);
}