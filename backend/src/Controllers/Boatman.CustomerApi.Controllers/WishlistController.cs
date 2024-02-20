using System.Net;
using Boatman.CustomerApi.UseCases.Commands.AddToWishlist;
using Boatman.CustomerApi.UseCases.Commands.RemoveFromWishlist;
using Boatman.Entities.Models.CustomerAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.CustomerApi.Controllers;

[Authorize(Policy = nameof(Customer))]
[ApiController]
[Route("[controller]")]
public class WishlistController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishlistController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddToWishlist([FromQuery] int apartmentId, [FromQuery] string customerId)
    {
        var response = await _mediator.Send(new AddToWishlistRequest(apartmentId, customerId));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("remove")]
    public async Task<IActionResult> RemoveFromWishlist([FromQuery] int apartmentId, [FromQuery] string customerId)
    {
        var response = await _mediator.Send(new RemoveFromWishlistRequest(apartmentId, customerId));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}