using GSN.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

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

        var weather1 = new WeatherForecast
        {
            Id = 1,
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            TemperatureC = 32,
            Summary = "Bright sunny day"
        };

        var weather2 = new WeatherForecast
        {
            Id = 2,
            Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
            TemperatureC = 32,
            Summary = "Rainy day"
        };

        var weatherForecasts = new List<WeatherForecast> { weather1, weather2 };

        return new OkObjectResult(weatherForecasts);
    }
}
