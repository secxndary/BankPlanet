using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes(Constants.ApplicationJson)]
[Produces(Constants.ApplicationJson)]
public class AuthenticationController(IServiceManager service) : ControllerBase
{
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IdentityError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUserAsync([FromBody] UserForRegistrationDto user, CancellationToken cancellationToken)
    {
        await service.AuthenticationService.RegisterUserAsync(user, cancellationToken);

        return Created();
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AuthenticateAsync([FromBody] UserForAuthenticationDto user, CancellationToken cancellationToken)
    {
        await service.AuthenticationService.ValidateUserAsync(user, cancellationToken);

        var tokenDto = await service.TokenService.CreateTokenAsync(populateExpiration: true, cancellationToken);

        return Ok(tokenDto);
    }
}