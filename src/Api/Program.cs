using Api;
using GSN.Application;
using GSN.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=BootstrapStartup.db"));

        services.AddSingleton(x => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        services.AddScoped<IRepository<WeatherForecast, int>, WeatherForecastRepository>();

    })
    .Build();

host.Run();
