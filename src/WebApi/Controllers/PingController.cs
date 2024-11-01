using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Dogs house service Version 1.0.1");
    }
}
