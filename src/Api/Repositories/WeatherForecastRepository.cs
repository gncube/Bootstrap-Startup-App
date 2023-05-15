using Api.Repositories;
using GSN.Application;
using GSN.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api;
internal class WeatherForecastRepository : Repository<WeatherForecast, int>, IWeatherForecastRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public WeatherForecastRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
