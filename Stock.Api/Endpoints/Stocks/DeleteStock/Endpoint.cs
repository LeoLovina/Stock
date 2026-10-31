using Stock.Api.Data;

namespace Stock.Api.Endpoints.Stocks;

public static class DeleteStockEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapDelete("/{id}", (string id, StockStore stocks) =>
            stocks.Delete(id) ? Results.NoContent() : Results.NotFound());
    }
}
