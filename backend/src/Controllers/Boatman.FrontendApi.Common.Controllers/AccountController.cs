using System.Net;
using Boatman.FrontendApi.Common.UseCases.Commands.EditProfile;
using Boatman.FrontendApi.Common.UseCases.Commands.EditProfilePhoto;
using Boatman.ProfileService.Interfaces.Dtos;
using Boatman.Utils.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    [HttpPost]
    [Route("edit-profile-photo")]
    public async Task<IActionResult> EditProfilePhoto([FromForm] [Image] IFormFile photo)
    {
        var response = await _mediator.Send(new EditProfilePhoto(photo, User));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}