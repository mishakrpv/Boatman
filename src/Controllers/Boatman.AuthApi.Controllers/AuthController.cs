using System.Net;
using Boatman.AuthApi.UseCases.Commands.Login;
using Boatman.AuthApi.UseCases.Commands.RegisterAsOwner;
using Boatman.AuthApi.UseCases.Dtos;
using Boatman.DataAccess.Identity.Interfaces.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.AuthApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("owner-register")]
    public async Task<IActionResult> RegisterAsOwner([FromBody] RegisterAsOwnerDto dto)
    {
        var response = await _mediator.Send(new RegisterAsOwnerRequest(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return response.Errors != null
            ? StatusCode(response.StatusCode, new { problem = response.Message, errors = response.Errors })
            : StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var response = await _mediator.Send(new LoginRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}