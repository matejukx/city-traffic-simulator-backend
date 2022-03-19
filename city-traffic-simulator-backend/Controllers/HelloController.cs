namespace city_traffic_simulator_backend.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/hello")]
public class HelloController : ControllerBase
{
    private readonly ILogger<HelloController> _logger;

    public HelloController(ILogger<HelloController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetHello")]
    public ObjectResult Get()
    {
        return this.Ok("hello");
    }
}