using System.Net;
using Boatman.FrontendApi.Customer.UseCases.Commands.SubmitRequest;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.FrontendApi.Customer.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    [Route("submit-request")]
    public async Task<IActionResult> SubmitRequest([FromQuery] int apartmentId, [FromQuery] string customerId)
    {
        var response = await _mediator.Send(new SubmitRequestRequest(apartmentId, customerId));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}