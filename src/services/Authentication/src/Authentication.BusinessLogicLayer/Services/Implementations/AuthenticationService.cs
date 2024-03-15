using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Authentication.BusinessLogicLayer.Services.Utility;
using Authentication.DataAccessLayer.Entities.Exceptions.BadRequest;
using Authentication.DataAccessLayer.Entities.Exceptions.MessagesConstants;
using Authentication.DataAccessLayer.Entities.Exceptions.NotFound;
using Authentication.DataAccessLayer.Entities.Exceptions.Unauthorized;
using Authentication.DataAccessLayer.Entities.Models;
using AutoMapper;
using Common.Constants;
using Common.Constants.LoggingMessagesConstants.Authentication;
using Common.Logging;
using Microsoft.AspNetCore.Identity;

namespace Authentication.BusinessLogicLayer.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly UserContext _userContext;
    private readonly ILoggerManager _logger;

    public AuthenticationService(IMapper mapper, UserManager<User> userManager, UserContext userContext, ILoggerManager logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task RegisterUserAsync(UserForRegistrationDto userForRegistration, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var registrationResult = await _userManager.CreateAsync(user, userForRegistration.Password!);

        if (!registrationResult.Succeeded)
        {
            _logger.LogError(AuthenticationLoggingMessages.RegisterUserAsyncError);
            throw new RegisterUserBadRequestException(string.Join(" ", registrationResult.Errors.Select(e => e.Description)));
        }

        _logger.LogInfo(AuthenticationLoggingMessages.RegisterUserAsyncSuccess);

        await _userManager.AddToRolesAsync(user, new[] { RoleConstants.User });
    }

    public async Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication, CancellationToken cancellationToken)
    {
        _userContext.User = await _userManager.FindByNameAsync(userForAuthentication.UserName!);

        if (_userContext.User is null)
        {
            _logger.LogError(AuthenticationLoggingMessages.ValidateUserAsyncError);
            throw new UserNotFoundException(ExceptionMessagesConstants.UserNotFound);
        }

        var validationResult = await _userManager.CheckPasswordAsync(_userContext.User, userForAuthentication.Password!);

        if (!validationResult)
        {
            _logger.LogError(AuthenticationLoggingMessages.ValidateUserAsyncError);
            throw new UnauthorizedException(ExceptionMessagesConstants.Unauthorized);
        }

        _logger.LogInfo(AuthenticationLoggingMessages.ValidateUserAsyncSuccess);

        return validationResult;
    }
}