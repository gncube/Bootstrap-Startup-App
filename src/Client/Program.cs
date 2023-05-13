using Client;
using Client.Services;
using Client.Services.Interfaces;
using GSN.Domain;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });

// Register services
builder.Services.AddScoped<IDataService<WeatherForecast, int>, WeatherService>();

// Add JsonSerilizerOptions service
builder.Services.AddSingleton(x => new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
});

await builder.Build().RunAsync();
