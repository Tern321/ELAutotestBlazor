using Microsoft.AspNetCore.Mvc;
using BlazorTest3.Shared;
using System.Text.RegularExpressions;

namespace BlazorTest3.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    string baseTestsPath = "/Users/evgeniiloshchenko/Documents/hostedApiFiles/autotestBackend/Tests/";

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("createTest/{testName}")]
    public IEnumerable<ELTest> createTest(string testName)
    {
        // string name
        Console.WriteLine("createTest");
        // clean name
        // create folder
        Regex rgx = new Regex("[^a-zA-Z0-9_ ]");
        testName = rgx.Replace(testName, "").Replace(" ", "_");
        if (testName.Length > 0 && testName.Length < 200)
        {
            Directory.CreateDirectory(baseTestsPath + testName);
        }

        return Get();
    }

    [HttpGet]
    public IEnumerable<ELTest> Get()
    {
        
        var testPaths = Directory.GetDirectories(baseTestsPath);

        List<ELTest> tests = new List<ELTest>();
        foreach (string path in testPaths)
        {
            var test = new ELTest();
            test.Name = Path.GetFileName(Path.GetDirectoryName(path + "/"));
            tests.Add(test);
        }
        return tests;
    }
    //[HttpGet]
    //public IEnumerable<ELTest> Get()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new ELTest
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}
}

