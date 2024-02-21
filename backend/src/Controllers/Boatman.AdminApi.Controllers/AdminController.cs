using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boatman.AdminApi.Controllers;

[Authorize(Policy = "Admin")]
[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    
}