using Stock.Api.Data;

namespace Stock.Api.Endpoints.Stocks;

public static class GetStockEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet("/{id}", (string id, StockStore stocks) =>
        {
            var stock = stocks.Get(id);
            return stock is null ? Results.NotFound() : Results.Ok(stock);
        }).WithName("GetStock");
    }
}
