using System.Net;
using Boatman.FrontendApi.Owner.UseCases.Commands.AddApartment;
using Boatman.FrontendApi.Owner.UseCases.Commands.AddPhoto;
using Boatman.FrontendApi.Owner.UseCases.Commands.DeleteApartment;
using Boatman.FrontendApi.Owner.UseCases.Commands.GetMyApartment;
using Boatman.FrontendApi.Owner.UseCases.Commands.UpdateApartment;
using Boatman.FrontendApi.Owner.UseCases.Dtos;
using Boatman.Utils.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.FrontendApi.Owner.Controllers;

[Authorize]
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
        var response = await _mediator.Send(new AddApartment(dto));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { id = response.Value });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateApartmentDto dto)
    {
        var response = await _mediator.Send(new UpdateApartment(dto));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });
        
        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var response = await _mediator.Send(new GetMyApartment(id));

        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(response.Value);

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteApartment(id));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("{id:int}/add-photo")]
    public async Task<IActionResult> AddPhoto([FromRoute] int id, [FromForm] [Image] IFormFile photo)
    {
        var response = await _mediator.Send(new AddPhoto(id, photo));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { uri = response.Value });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}