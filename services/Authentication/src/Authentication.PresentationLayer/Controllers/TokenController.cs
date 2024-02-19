using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes(Constants.ApplicationJson)]
[Produces(Constants.ApplicationJson)]
public class TokenController(IServiceManager service) : ControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto, CancellationToken cancellationToken)
    {
        var tokenDtoRefreshed = await service.TokenService.RefreshTokenAsync(tokenDto, cancellationToken);
        return Ok(tokenDtoRefreshed);
    }
}