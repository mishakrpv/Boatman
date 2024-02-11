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
}