using Stock.Api.Data;

namespace Stock.Api.Endpoints.Stocks;

public static class UpdateStockEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPut("/{id}", (string id, UpdateStockRequest request, StockStore stocks) =>
        {
            if (string.IsNullOrWhiteSpace(request.Symbol) || string.IsNullOrWhiteSpace(request.Name))
            {
                return Results.BadRequest(new { error = "Both symbol and name are required." });
            }

            var stock = stocks.Update(id, request.Symbol, request.Name);
            return stock is null ? Results.NotFound() : Results.Ok(stock);
        });
    }
}
