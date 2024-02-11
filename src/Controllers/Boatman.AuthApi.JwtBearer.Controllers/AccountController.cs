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
    [Route("/[controller]/Signup/Owner")]
    public async Task<TokenPair> RegisterAsOwner([FromBody] SignUpAsOwnerDto dto)
    {
        var tokenPair = await _mediator.Send(new SignUpAsOwnerRequest(dto));

        return tokenPair;
    }
}