using Microsoft.AspNetCore.Authentication.JwtBearer;
using Stock.Api.Data;
using Stock.Api.Endpoints.Stocks;

var builder = WebApplication.CreateBuilder(args);

var auth0Domain = builder.Configuration["Auth0:Domain"];
var auth0Audience = builder.Configuration["Auth0:Audience"];

if (string.IsNullOrWhiteSpace(auth0Domain) || string.IsNullOrWhiteSpace(auth0Audience))
{
    throw new InvalidOperationException(
        "Auth0 configuration is required. Set Auth0:Domain and Auth0:Audience (or Auth0__Domain and Auth0__Audience).");
}

var authority = auth0Domain.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
    ? auth0Domain.TrimEnd('/')
    : $"https://{auth0Domain.Trim().TrimEnd('/')}";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = authority;
        options.Audience = auth0Audience;
    });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<StockStore>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapStockEndpoints();

app.Run();
