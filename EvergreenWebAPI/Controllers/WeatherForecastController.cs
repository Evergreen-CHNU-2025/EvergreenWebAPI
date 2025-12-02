using Microsoft.AspNetCore.Mvc;

namespace EvergreenWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("FindMuzhForAnna")]
    public string GetInfo()
        => "¿Ìˇ, Ì‡È‰Ë ÒÓ·≥ ÏÛÊ‡";

    [HttpGet("Baza")]
    public string GetBlondeInfo()
        => "¡À¿Õƒ≤Õ » “Œœ!";
}