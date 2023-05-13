using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Api;

public class HelloWorldApi
{
    private readonly ILogger _logger;

    public HelloWorldApi(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HelloWorldApi>();
    }

    [Function(nameof(HelloWorldGet))]
    public async Task<IActionResult> HelloWorldGet(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "helloworld")] HttpRequestData req)
    {
        _logger.LogInformation($"{nameof(HelloWorldGet)} function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);

        var weather = new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            TemperatureC = 32,
            Summary = "Bright sunny day"
        };

        return new OkObjectResult(weather);
    }

    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
