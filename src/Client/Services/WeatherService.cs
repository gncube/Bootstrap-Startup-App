using Client.Services.Interfaces;
using GSN.Domain;
using System.Text.Json;

namespace Client.Services;

public class WeatherService : IDataService<WeatherForecast, int>
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public WeatherService(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = jsonSerializerOptions;
    }
    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        using var responseStream = await _httpClient.GetStreamAsync($"api/helloworld");
        using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

        var weatherForecastArray = jsonDoc.RootElement.GetProperty("Value");
        var weatherForecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(weatherForecastArray.GetRawText(), _jsonSerializerOptions);

        return weatherForecasts ?? Enumerable.Empty<WeatherForecast>();
    }

    public Task<WeatherForecast> GetAsync(int key)
    {
        throw new NotImplementedException();
    }

    public Task Update(WeatherForecast t)
    {
        throw new NotImplementedException();
    }
}
