using Authentication.BusinessLogicLayer.Services.Interfaces;
using Authentication.DataAccessLayer.Entities.ConfigurationModels;
using Authentication.DataAccessLayer.Entities.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Authentication.BusinessLogicLayer.Services.Implementations;

public class ServiceManager(
    IMapper mapper,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptionsSnapshot<JwtConfiguration> config)
    : IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authenticationService = 
        new(() => new AuthenticationService(mapper, userManager, roleManager, config));

    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}