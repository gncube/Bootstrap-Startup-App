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

// Register HttpClient service using HttpClientFactory
builder.Services.AddHttpClient<IBlogDataService, BlogDataService>(client =>
    client.BaseAddress = new Uri(builder.Configuration["BlogAPI_Prefix"] ?? builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient<IDataService<WeatherForecast, int>, WeatherService>(client =>
    client.BaseAddress = new Uri(builder.Configuration["WeatherAPI_Prefix"] ?? builder.HostEnvironment.BaseAddress));

// Add JsonSerilizerOptions service
builder.Services.AddSingleton(x => new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
});

await builder.Build().RunAsync();
