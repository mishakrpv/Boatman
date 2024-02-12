using Boatman.Entities.Models.ApartmentAggregate;
using Boatman.OwnerApi.UseCases.Commands.AddApartment;
using Boatman.OwnerApi.UseCases.Commands.GetApartment;
using Boatman.OwnerApi.UseCases.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.OwnerApi.Controllers;

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
    public async Task<IActionResult> Add([FromBody] AddApartmentDto dto)
    {
        var id = await _mediator.Send(new AddApartmentRequest(dto));

        return Ok(new { id = id });
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<Apartment> Get(int id)
    {
        var apartment = await _mediator.Send(new GetApartmentRequest(id));

        return apartment;
    }
}