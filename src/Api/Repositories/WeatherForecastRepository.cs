using GSN.Application;
using GSN.Domain;

namespace Api.Repositories;
internal class WeatherForecastRepository : Repository<WeatherForecast, int>, IWeatherForecastRepository
{
    private readonly AppDbContext _context;

    public WeatherForecastRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
