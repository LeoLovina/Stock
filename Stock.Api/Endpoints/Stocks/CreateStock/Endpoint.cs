using Stock.Api.Data;

namespace Stock.Api.Endpoints.Stocks;

public static class CreateStockEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost("", (CreateStockRequest request, StockStore stocks) =>
        {
            if (string.IsNullOrWhiteSpace(request.Symbol) || string.IsNullOrWhiteSpace(request.Name))
            {
                return Results.BadRequest(new { error = "Both symbol and name are required." });
            }

            var stock = stocks.Create(request.Symbol, request.Name);
            return Results.CreatedAtRoute("GetStock", new { id = stock.Id }, stock);
        });
    }
}
