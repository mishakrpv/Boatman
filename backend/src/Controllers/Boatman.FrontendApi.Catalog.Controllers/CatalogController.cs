using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.FrontendApi.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    
}