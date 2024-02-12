using Boatman.AuthApi.UseCases.Commands.SignUpAsOwner;
using Boatman.AuthApi.UseCases.Dtos;
using Boatman.TokenService.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.AuthApi.JwtBearer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("/[controller]/SignUp/Owner")]
    public async Task<IActionResult> RegisterAsOwner([FromBody] SignUpAsOwnerDto dto)
    {
        bool isSuccess = await _mediator.Send(new SignUpAsOwnerRequest(dto));

        if (isSuccess)
            return Ok();
        else
            return BadRequest("User creation failed! Please check user details and try again.");
    }
}