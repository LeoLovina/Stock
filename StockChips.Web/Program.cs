using StockChips.Web.ApiClients;
using StockChips.Web.Endpoints.Stocks;
using StockChips.Web.Extensions;
using StockChips.Web.Options;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FinMindOptions>(builder.Configuration.GetSection(FinMindOptions.SectionName));
builder.Services.AddHttpClient<FinMindClient>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("ClientApp");
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/health", () => Results.Ok(new { status = "ok" }));
app.MapStockEndpoints();

app.MapFallbackToFile("index.html");

app.Run();
