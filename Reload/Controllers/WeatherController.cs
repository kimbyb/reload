using Microsoft.AspNetCore.Mvc;
using Reload.Services;

namespace Reload.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly ITimeService _timeService;
    private readonly GenericCalculator<string> _calculator = new();
    private readonly LambdaProcessor _lambdaProcessor = new();

    public WeatherController(ITimeService timeService)
    {
        _timeService = timeService;
    }

    [HttpGet]
    public string Get()
    {
        // conditional 
        // if (DateTime.Now.Second % 2 == 0)
        // {
        //     return $"Even second - {_timeService.GetMessage()}";
        // }
        //
        // return $"Odd second - {_timeService.GetMessage()}";
        
        return $"Weather API response - {_timeService.GetMessage()}";
        
        //with generic
        //return _calculator.Echo($"Weather API response - {_timeService.GetMessage()}");
        
        //with lambdas
        //var result = _lambdaProcessor.Process(5, x => x * 2);
        //return $"Lambda result: {result}";
    }
    
 //   response type HR-24
     // [HttpGet]
     // public IActionResult Get()
     // {
     //     return Ok(new
     //     {
     //         Message = "Weather API response HR-24",
     //         Time = _timeService.GetMessage()
     //     });
     // }

    //extra route will work on HR
    // [HttpGet("version")]
    // public string Version()
    // {
    //     return "v2";
    // }

   // }
}