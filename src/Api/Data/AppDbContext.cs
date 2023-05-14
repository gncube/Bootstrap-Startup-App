using GSN.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api;
internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>().HasData(
            new WeatherForecast
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                TemperatureC = 32,
                Summary = "Bright sunny day"
            });
        base.OnModelCreating(modelBuilder);
    }
}
