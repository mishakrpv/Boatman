using System.Net;
using Boatman.CustomerApi.UseCases.Commands.SendRequest;
using Boatman.CustomerApi.UseCases.Dtos;
using Boatman.Entities.Models.CustomerAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.CustomerApi.Controllers;

[Authorize(Policy = nameof(Customer))]
[ApiController]
[Route("[controller]/[action]")]
public class ApartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("send-request")]
    public async Task<IActionResult> SendRequest([FromQuery] SendRequestDto dto)
    {
        var response = await _mediator.Send(new SendRequestRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}