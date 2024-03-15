using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.PresentationLayer.Controllers;

[ApiController]
[Route(Constants.ApiController)]
[Consumes(Constants.ApplicationJson)]
[Produces(Constants.ApplicationJson)]
public class TokenController(IServiceManager service) : ControllerBase
{
    [Authorize]
    [HttpPost(Constants.Refresh)]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshAsync([FromBody] TokenDto tokenDto, CancellationToken cancellationToken)
    {
        var tokenDtoRefreshed = await service.TokenService.RefreshTokenAsync(tokenDto, cancellationToken);

        return Ok(tokenDtoRefreshed);
    }
}