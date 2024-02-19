using Authentication.BusinessLogicLayer.Services.Interfaces;

namespace Authentication.BusinessLogicLayer.Services.Implementations;

public class ServiceManager(IAuthenticationService authenticationService, ITokenService tokenService) : IServiceManager
{
    public IAuthenticationService AuthenticationService => authenticationService;
    public ITokenService TokenService => tokenService;
}