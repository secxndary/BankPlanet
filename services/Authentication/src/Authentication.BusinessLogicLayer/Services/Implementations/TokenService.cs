using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Authentication.BusinessLogicLayer.Services.Utility;
using Authentication.DataAccessLayer.Entities.ConfigurationModels;
using Authentication.DataAccessLayer.Entities.Exceptions.BadRequest;
using Authentication.DataAccessLayer.Entities.Models;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.BusinessLogicLayer.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly UserContext _userContext;

    public TokenService(IMapper mapper, UserManager<User> userManager, IOptionsSnapshot<JwtConfiguration> configuration, UserContext userContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _jwtConfiguration = configuration.Value;
        _userContext = userContext;
    }


    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalForExpiredToken(tokenDto.AccessToken);
        var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

        if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new RefreshTokenBadRequest();
        }

        _userContext.User = user;

        return await CreateTokenAsync(populateExpiration: false, cancellationToken);
    }

    public async Task<TokenDto> CreateTokenAsync(bool populateExpiration, CancellationToken cancellationToken)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(_userContext.User!);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        var refreshToken = GenerateRefreshToken();
        _userContext.User!.RefreshToken = refreshToken;

        if (populateExpiration)
        {
            _userContext.User.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(_jwtConfiguration.RefreshExpiresDays));
        }

        await _userManager.UpdateAsync(_userContext.User);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto(accessToken, refreshToken);
    }

    public async Task<UserDto> GetUserByTokenAsync(TokenDto tokenDto, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalForExpiredToken(tokenDto.AccessToken);

        var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new RefreshTokenBadRequest();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Roles = roles.ToList();

        return userDto;
    }


    private ClaimsPrincipal GetPrincipalForExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = _jwtConfiguration.ValidateLifetime,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(Constants.Secret)!))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals
                (SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException(ExceptionMessagesConstants.SecurityToken);
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(Constants.Secret)!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user!.UserName!)
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _jwtConfiguration.ValidIssuer,
            audience: _jwtConfiguration.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.AccessExpiresMinutes)),
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}