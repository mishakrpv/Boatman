using Boatman.AuthApi.UseCases.Commands.RefreshToken;
using Boatman.TokenService.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.AuthApi.JwtBearer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("/[controller]/Token/Refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenPair tokenPair)
    {
        var newTokenPair = await _mediator.Send(new RefreshTokenRequest(tokenPair));

        if (newTokenPair != null)
        {
            return Ok(newTokenPair);
        }

        return BadRequest();
    }
}