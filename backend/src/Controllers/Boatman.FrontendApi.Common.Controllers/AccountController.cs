using System.Net;
using Boatman.FrontendApi.Common.UseCases.Commands.EditProfile;
using Boatman.ProfileService.Interfaces.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.FrontendApi.Common.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("edit-profile")]
    public async Task<IActionResult> EditProfile([FromBody] PersonalDataDto dto)
    {
        var response = await _mediator.Send(new EditProfile(
            new PersonalDataWithPrincipalDto(dto) { Principal = User }));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}