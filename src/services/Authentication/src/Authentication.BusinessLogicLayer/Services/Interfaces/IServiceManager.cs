namespace Authentication.BusinessLogicLayer.Services.Interfaces;

public interface IServiceManager
{
    IAuthenticationService AuthenticationService { get; }
    ITokenService TokenService { get; }
}