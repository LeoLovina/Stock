using Stock.Api.Data;

namespace Stock.Api.Endpoints.Stocks;

public static class ListStocksEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapGet("", (StockStore stocks) => Results.Ok(stocks.GetAll()));
    }
}
