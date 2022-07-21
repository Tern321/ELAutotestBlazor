using Microsoft.AspNetCore.Mvc;
using BlazorTest3.Shared;

namespace BlazorTest3.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    //[HttpGet]
    //public IEnumerable<ELAutotestObject> Get()
    //{
    //    var baseTestsPath = "Users/evgeniiloshchenko/Documents/hostedApiFiles/autotestBackend/Tests/";
    //    var testPaths = Directory.GetDirectories(baseTestsPath);

    //    List<ELAutotestObject> tests = new List<ELAutotestObject>();
    //    foreach (string path in testPaths)
    //    {
    //        var test = new ELAutotestObject();
    //        test.Name = Path.GetFileName(Path.GetDirectoryName(path));
    //        tests.Add(test);
    //    }
    //    return tests;
    //}
        [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

