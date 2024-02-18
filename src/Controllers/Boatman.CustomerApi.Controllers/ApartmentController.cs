using System.Net;
using Boatman.CustomerApi.UseCases.Commands.AddToWishlist;
using Boatman.CustomerApi.UseCases.Commands.RemoveFromWishlist;
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
    public async Task<IActionResult> SendRequest([FromQuery] ApartmentCustomerDto dto)
    {
        var response = await _mediator.Send(new SendRequestRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("wishlist-add")]
    public async Task<IActionResult> AddToWishlist([FromQuery] ApartmentCustomerDto dto)
    {
        var response = await _mediator.Send(new AddToWishlistRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("wishlist-remove")]
    public async Task<IActionResult> RemoveFromWishlist([FromQuery] ApartmentCustomerDto dto)
    {
        var response = await _mediator.Send(new RemoveFromWishlistRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}