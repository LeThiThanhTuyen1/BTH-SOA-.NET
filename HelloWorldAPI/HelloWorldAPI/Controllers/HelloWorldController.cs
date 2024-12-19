using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet("auth")]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }
}
