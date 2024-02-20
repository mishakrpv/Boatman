using System.Net;
using Boatman.CustomerApi.UseCases.Commands.SubmitAnApplication;
using Boatman.Entities.Models.CustomerAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.CustomerApi.Controllers;

[Authorize(Policy = nameof(Customer))]
[ApiController]
[Route("[controller]")]
public class ApartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("submit-an-application")]
    public async Task<IActionResult> SubmitAnApplication([FromQuery] int apartmentId, [FromQuery] string customerId)
    {
        var response = await _mediator.Send(new SubmitAnApplicationRequest(apartmentId, customerId));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}