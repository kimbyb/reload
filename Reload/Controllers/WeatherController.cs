using Microsoft.AspNetCore.Mvc;
using Reload.Services;

namespace Reload.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly ITimeService _timeService;

    public WeatherController(ITimeService timeService)
    {
        _timeService = timeService;
    }

    [HttpGet]
    public string Get()
    {
        return $"Weather API response - {_timeService.GetMessage()}";
    }
}