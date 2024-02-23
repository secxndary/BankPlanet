using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Authentication.BusinessLogicLayer.Services.Utility;
using Authentication.DataAccessLayer.Entities.Exceptions.MessagesConstants;
using Authentication.DataAccessLayer.Entities.Exceptions.NotFound;
using Authentication.DataAccessLayer.Entities.Exceptions.Unauthorized;
using Authentication.DataAccessLayer.Entities.Models;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Identity;

namespace Authentication.BusinessLogicLayer.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly UserContext _userContext;

    public AuthenticationService(IMapper mapper, UserManager<User> userManager, UserContext userContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userContext = userContext;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var registrationResult = await _userManager.CreateAsync(user, userForRegistration.Password!);

        if (registrationResult.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, new[] { RoleConstants.User });
        }

        return registrationResult;
    }

    public async Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication, CancellationToken cancellationToken)
    {
        _userContext.User = await _userManager.FindByNameAsync(userForAuthentication.UserName!);

        if (_userContext.User is null)
        {
            throw new UserNotFoundException(ExceptionMessagesConstants.UserNotFound);
        }

        var validationResult = await _userManager.CheckPasswordAsync(_userContext.User, userForAuthentication.Password!);

        if (!validationResult)
        {
            throw new UnauthorizedException(ExceptionMessagesConstants.Unauthorized);
        }

        return validationResult;
    }
}