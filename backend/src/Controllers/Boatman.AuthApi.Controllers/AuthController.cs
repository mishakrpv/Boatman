using System.Net;
using Boatman.AuthApi.UseCases.Commands.ConfirmEmail;
using Boatman.AuthApi.UseCases.Commands.ForgetPassword;
using Boatman.AuthApi.UseCases.Commands.Login;
using Boatman.AuthApi.UseCases.Commands.Register;
using Boatman.AuthApi.UseCases.Commands.ResetPassword;
using Boatman.AuthService.Interfaces.Dtos;
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
    [Route("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var response = await _mediator.Send(new RegisterRequest(dto));

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

    [HttpGet]
    [Route("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto dto)
    {
        var response = await _mediator.Send(new ConfirmEmailRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return response.Errors != null
            ? StatusCode(response.StatusCode, new { problem = response.Message, errors = response.Errors })
            : StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("forget-password")]
    public async Task<IActionResult> ForgetPassword(string email)
    {
        var response = await _mediator.Send(new ForgetPasswordRequest(email));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var response = await _mediator.Send(new ResetPasswordRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return response.Errors != null
            ? StatusCode(response.StatusCode, new { problem = response.Message, errors = response.Errors })
            : StatusCode(response.StatusCode, new { problem = response.Message });
    }
}