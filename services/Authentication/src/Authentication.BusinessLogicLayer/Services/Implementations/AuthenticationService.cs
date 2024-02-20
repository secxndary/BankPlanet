using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Authentication.BusinessLogicLayer.Services.Utility;
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
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserContext _userContext;

    public AuthenticationService(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, UserContext userContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _userContext = userContext;
    }


    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password!);

        foreach (var role in userForRegistration.Roles!)
            if (!await _roleManager.RoleExistsAsync(role))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotExist",
                    Description = $"Role {role} does not exist."
                });
            }

        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles!);
        }

        return result;
    }

    public async Task<bool> ValidateUserAsync(UserForAuthenticationDto userForAuthentication, CancellationToken cancellationToken)
    {
        _userContext.User = await _userManager.FindByNameAsync(userForAuthentication.UserName!);

        var result = _userContext.User != null && await _userManager.CheckPasswordAsync(_userContext.User, userForAuthentication.Password!);

        return result ? result : throw new UnauthorizedException(ExceptionMessagesConstants.Unauthorized);
    }
}