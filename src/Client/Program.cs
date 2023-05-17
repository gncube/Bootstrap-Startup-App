using Blazored.LocalStorage;
using Client;
using Client.Services;
using Client.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TODO: builder.HostEnvironment.BaseAddress
//TODO: builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress

builder.Services.AddHttpClient<IBlogDataService, BlogDataService>(
    bds => bds.BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddScoped<ApplicationState>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});

await builder.Build().RunAsync();
