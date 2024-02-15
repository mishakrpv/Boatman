using System.Net;
using Boatman.OwnerApi.UseCases.Commands.AddApartment;
using Boatman.OwnerApi.UseCases.Commands.GetApartment;
using Boatman.OwnerApi.UseCases.Commands.GetSchedule;
using Boatman.OwnerApi.UseCases.Commands.ScheduleViewing;
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
        var response = await _mediator.Send(new AddApartmentRequest(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { id = response.Value });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetApartmentRequest(id));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("/[controller]/schedule/{id:int}")]
    public async Task<IActionResult> GetSchedule(int id)
    {
        var response = await _mediator.Send(new GetScheduleRequest(id));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("/[controller]/schedule-viewing")]
    public async Task<IActionResult> ScheduleViewing([FromBody] PlanViewingDto dto)
    {
        var response = await _mediator.Send(new ScheduleViewingRequest(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}