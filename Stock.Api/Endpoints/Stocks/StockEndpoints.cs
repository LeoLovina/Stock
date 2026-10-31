namespace Stock.Api.Endpoints.Stocks;

public static class StockEndpoints
{
    public static IEndpointRouteBuilder MapStockEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/stocks").RequireAuthorization();

        ListStocksEndpoint.Map(group);
        GetStockEndpoint.Map(group);
        CreateStockEndpoint.Map(group);
        UpdateStockEndpoint.Map(group);
        DeleteStockEndpoint.Map(group);

        return app;
    }
}
