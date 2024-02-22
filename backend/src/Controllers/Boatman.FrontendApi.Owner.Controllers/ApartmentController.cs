using System.Net;
using Boatman.FrontendApi.Owner.UseCases.Commands.AddApartment;
using Boatman.FrontendApi.Owner.UseCases.Commands.CancelViewing;
using Boatman.FrontendApi.Owner.UseCases.Commands.DeleteApartment;
using Boatman.FrontendApi.Owner.UseCases.Commands.GetApartment;
using Boatman.FrontendApi.Owner.UseCases.Commands.GetSchedule;
using Boatman.FrontendApi.Owner.UseCases.Commands.ScheduleViewing;
using Boatman.FrontendApi.Owner.UseCases.Commands.UpdateApartment;
using Boatman.FrontendApi.Owner.UseCases.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.FrontendApi.Owner.Controllers;

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
    [Route("[action]")]
    public async Task<IActionResult> Add([FromBody] AddApartmentDto dto)
    {
        var response = await _mediator.Send(new AddApartmentRequest(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { id = response.Value });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateApartmentDto dto)
    {
        var response = await _mediator.Send(new UpdateApartmentRequest(dto));
        
        if (response.StatusCode != (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });
        
        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetApartmentRequest(id));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("{id:int}/schedule")]
    public async Task<IActionResult> GetSchedule([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetScheduleRequest(id));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("schedule-viewing")]
    public async Task<IActionResult> ScheduleViewing([FromBody] ScheduleViewingDto dto)
    {
        var response = await _mediator.Send(new ScheduleViewingRequest(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("cancel-viewing")]
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

    // [HttpPost]
    // [Route("attach-photos")]
    // public async Task<IActionResult> AttachPhotos(IFormFileCollection files)
    // {
    //     
    // }
}