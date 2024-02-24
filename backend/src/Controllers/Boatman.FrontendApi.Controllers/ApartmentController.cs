using System.Net;
using Boatman.FrontendApi.UseCases.Commands.AddApartment;
using Boatman.FrontendApi.UseCases.Commands.AddPhoto;
using Boatman.FrontendApi.UseCases.Commands.DeleteApartment;
using Boatman.FrontendApi.UseCases.Commands.GetApartment;
using Boatman.FrontendApi.UseCases.Commands.UpdateApartment;
using Boatman.FrontendApi.UseCases.Dtos;
using Boatman.Utils.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.FrontendApi.Controllers;

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
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
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

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteApartmentRequest(id));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { message = response.Message });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }

    [HttpPost]
    [Route("{id:int}/add-photo")]
    public async Task<IActionResult> AddPhoto([FromRoute] int id, [FromForm] [Image] IFormFile photo)
    {
        var response = await _mediator.Send(new AddPhotoRequest(id, photo));
        
        if (response.StatusCode == (int)HttpStatusCode.OK)
            return Ok(new { uri = response.Value });

        return StatusCode(response.StatusCode, new { problem = response.Message });
    }
}