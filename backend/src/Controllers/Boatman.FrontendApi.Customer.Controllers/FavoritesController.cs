using System.Net;
using Boatman.FrontendApi.Customer.UseCases.Commands.AddToFavorites;
using Boatman.FrontendApi.Customer.UseCases.Commands.RemoveFromFavorites;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.FrontendApi.Customer.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]/[action]")]
public class FavoritesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FavoritesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromQuery] int apartmentId, [FromQuery] string customerId)
    {
        var response = await _mediator.Send(new AddToFavoritesRequest(apartmentId, customerId));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    public async Task<IActionResult> Remove([FromQuery] int apartmentId, [FromQuery] string customerId)
    {
        var response = await _mediator.Send(new RemoveFromFavoritesRequest(apartmentId, customerId));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}