using Authentication.BusinessLogicLayer.DataTransferObjects;

namespace Authentication.BusinessLogicLayer.Services.Interfaces;

public interface ITokenService
{
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto, CancellationToken cancellationToken);
    Task<TokenDto> CreateTokenAsync(bool populateExpiration, CancellationToken cancellationToken);
    Task<UserDto> GetUserByTokenAsync(TokenDto tokenDto, CancellationToken cancellationToken);
}