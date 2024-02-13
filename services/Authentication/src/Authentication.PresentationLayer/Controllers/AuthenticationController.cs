using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.PresentationLayer.Controllers;

[ApiController]
[Route("api/auth")]
[Consumes(Constants.ApplicationJson)]
[Produces(Constants.ApplicationJson)]
public class AuthenticationController(IServiceManager service) : ControllerBase
{
    [HttpPost("sign-up")]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(IdentityError), 400)]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto user)
    {
        var result = await service.AuthenticationService.RegisterUser(user);
        if (result.Succeeded) return StatusCode(201);

        foreach (var error in result.Errors)
            ModelState.TryAddModelError(error.Code, error.Description);
        return BadRequest(ModelState);
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(TokenDto), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        if (!await service.AuthenticationService.ValidateUser(user))
            return Unauthorized();

        var tokenDto = await service.AuthenticationService.CreateToken(populateExpiration: true);
        return Ok(tokenDto);
    }
}