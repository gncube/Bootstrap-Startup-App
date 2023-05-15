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

// Register HttpClient service using HttpClientFactory
builder.Services.AddHttpClient<IBlogDataService, BlogDataService>(
       bds => bds.BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient<IDataService<WeatherForecast, int>, WeatherService>(
    ws => ws.BaseAddress = new Uri(builder.Configuration["WeatherAPI_Prefix"] ?? builder.HostEnvironment.BaseAddress));

// Add JsonSerilizerOptions service
builder.Services.AddSingleton(x => new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
});

await builder.Build().RunAsync();
