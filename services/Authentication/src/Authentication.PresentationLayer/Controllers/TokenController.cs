using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.PresentationLayer.Controllers;

[ApiController]
[Route("api/token")]
[Consumes(Constants.ApplicationJson)]
[Produces(Constants.ApplicationJson)]
public class TokenController(IServiceManager service) : ControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoRefreshed = await service.AuthenticationService.RefreshToken(tokenDto);
        return Ok(tokenDtoRefreshed);
    }

    [HttpPost("get-user")]
    [ProducesResponseType(typeof(TokenDto), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetUserByToken([FromBody] TokenDto token)
    {
        var user = await service.AuthenticationService.GetUserByToken(token);
        return Ok(user);
    }
}