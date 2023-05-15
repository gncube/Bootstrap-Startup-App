using Api;
using Api.Repositories;
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
        //services.AddDbContextFactory<AppDbContext>(options =>
        //    options.UseSqlite("Data Source=BootstrapStartup.db"));

        services.AddScoped<AppDbContext>(svc =>
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite("Data Source=BootstrapStartup.db");
            return new AppDbContext(options.Options);
        });

        services.AddSingleton(x => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        services.AddScoped<IRepository<WeatherForecast, int>, WeatherForecastRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();

    })
    .Build();

host.Run();
