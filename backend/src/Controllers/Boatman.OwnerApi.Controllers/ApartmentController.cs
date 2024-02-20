using System.Net;
using Boatman.Entities.Models.OwnerAggregate;
using Boatman.OwnerApi.UseCases.Commands.AddApartment;
using Boatman.OwnerApi.UseCases.Commands.CancelViewing;
using Boatman.OwnerApi.UseCases.Commands.DeleteApartment;
using Boatman.OwnerApi.UseCases.Commands.GetApartment;
using Boatman.OwnerApi.UseCases.Commands.GetSchedule;
using Boatman.OwnerApi.UseCases.Commands.ScheduleViewing;
using Boatman.OwnerApi.UseCases.Commands.UpdateApartment;
using Boatman.OwnerApi.UseCases.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.OwnerApi.Controllers;

[Authorize(Policy = nameof(Owner))]
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
        var response = await _mediator.Send(new AddApartmentRequest(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { id = response.Value });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateApartmentDto dto)
    {
        var response = await _mediator.Send(new UpdateApartmentRequest(dto));
        
        if (response.StatusCode != (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });
        
        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("/[controller]/{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetApartmentRequest(id));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("/[controller]/{id:int}/schedule")]
    public async Task<IActionResult> GetSchedule(int id)
    {
        var response = await _mediator.Send(new GetScheduleRequest(id));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("/[controller]/schedule-viewing")]
    public async Task<IActionResult> ScheduleViewing([FromBody] ScheduleViewingDto dto)
    {
        var response = await _mediator.Send(new ScheduleViewingRequest(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("/[controller]/cancel-viewing")]
    public async Task<IActionResult> CancelViewing([FromQuery] CancelViewingDto dto)
    {
        var response = await _mediator.Send(new CancelViewingRequest(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteApartmentRequest(id));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}