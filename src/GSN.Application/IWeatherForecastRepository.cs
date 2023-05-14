using GSN.Domain;

namespace GSN.Application;
public interface IWeatherForecastRepository : IRepository<WeatherForecast, int>
{
}
