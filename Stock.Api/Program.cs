using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Paste an Auth0 access token for the Stock API."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock API v1");
    options.RoutePrefix = string.Empty;
});
app.MapStockEndpoints();

app.Run();
