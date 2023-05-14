using GSN.Application;
using GSN.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Api;

public class HelloWorldApi
{
    private readonly ILogger _logger;
    private readonly IRepository<WeatherForecast, int> _repo;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public HelloWorldApi(ILoggerFactory loggerFactory, IRepository<WeatherForecast, int> repo, JsonSerializerOptions jsonSerializerOptions)
    {
        _logger = loggerFactory.CreateLogger<HelloWorldApi>();
        _repo = repo;
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    [Function(nameof(HelloWorldGet))]
    public async Task<IActionResult> HelloWorldGet(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "helloworld")] HttpRequestData req)
    {
        _logger.LogInformation($"{nameof(HelloWorldGet)} function processed a request.");

        var weatherForecasts = await _repo.GetAllAsync();
        return new OkObjectResult(weatherForecasts);
    }
}
