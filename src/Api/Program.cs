using Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var home = Environment.GetEnvironmentVariable("HOME") ?? "";
var databasePath = Path.Combine(home, "database.sqlite");


var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<BlogifierDbContext>(svc =>
        {
            var options = new DbContextOptionsBuilder<BlogifierDbContext>().UseSqlite($"Data Source={databasePath}");
            return new BlogifierDbContext(options.Options);
        });

        services.AddScoped<IBlogRepository, BlogRepository>();

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        //}).AddJwtBearer(options =>
        //{
        //    options.Authority = "https://dev-fqw5tfm880p64hwo.uk.auth0.com/";
        //    options.Audience = "https://BudgetAzureFunctionsApi.com/api";
        //});

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = $"https://dev-fqw5tfm880p64hwo.uk.auth0.com/";
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = "https://BudgetAzureFunctionsApi.com/api",
                    ValidIssuer = "dev-fqw5tfm880p64hwo.uk.auth0.com"
                };
            });
    })
    .Build();


host.Run();
