using System.Text.Json;
using StockChips.Web.ApiClients;

namespace StockChips.Web.Endpoints.Stocks;

public static class StockEndpoints
{
    public static IEndpointRouteBuilder MapStockEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/stocks");

        group.MapGet("/search", SearchStocksAsync);
        group.MapGet("/{stockId}/chips", GetStockChipsAsync);

        return app;
    }

    private static async Task<IResult> SearchStocksAsync(
        string? query,
        FinMindClient finMind,
        CancellationToken cancellationToken)
    {
        var normalizedQuery = query?.Trim() ?? string.Empty;
        var rows = await finMind.GetDataAsync("TaiwanStockInfo", null, null, cancellationToken);

        var stocks = rows
            .Select(StockSearchResult.FromFinMind)
            .Where(stock => stock is not null)
            .Select(stock => stock!)
            .Where(stock => stock.Market is "twse" or "tpex")
            .Where(stock =>
                string.IsNullOrWhiteSpace(normalizedQuery) ||
                stock.StockId.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ||
                stock.StockName.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase))
            .OrderBy(stock => stock.StockId)
            .Take(20)
            .ToArray();

        return Results.Ok(stocks);
    }

    private static async Task<IResult> GetStockChipsAsync(
        string stockId,
        FinMindClient finMind,
        CancellationToken cancellationToken)
    {
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-45));

        var institutionalRows = await finMind.GetDataAsync(
            "InstitutionalInvestorsBuySell",
            stockId,
            startDate,
            cancellationToken);

        var marginRows = await finMind.GetDataAsync(
            "TaiwanStockMarginPurchaseShortSale",
            stockId,
            startDate,
            cancellationToken);

        var holdingRows = await finMind.GetDataAsync(
            "TaiwanStockHoldingSharesPer",
            stockId,
            startDate,
            cancellationToken);

        var response = StockChipDashboard.FromFinMind(stockId, institutionalRows, marginRows, holdingRows);
        return Results.Ok(response);
    }
}

public sealed record StockSearchResult(
    string StockId,
    string StockName,
    string Market,
    string IndustryCategory)
{
    public static StockSearchResult? FromFinMind(JsonElement row)
    {
        var stockId = row.GetString("stock_id");
        var stockName = row.GetString("stock_name");
        var market = row.GetString("type");

        if (string.IsNullOrWhiteSpace(stockId) ||
            string.IsNullOrWhiteSpace(stockName) ||
            string.IsNullOrWhiteSpace(market))
        {
            return null;
        }

        return new StockSearchResult(
            stockId,
            stockName,
            market,
            row.GetString("industry_category") ?? string.Empty);
    }
}

public sealed record StockChipDashboard(
    string StockId,
    string LatestDataDate,
    IReadOnlyList<InstitutionalInvestorRow> InstitutionalInvestors,
    IReadOnlyList<MarginShortRow> MarginShort,
    IReadOnlyList<HoldingShareRow> HoldingShares,
    IReadOnlyList<string> Signals)
{
    public static StockChipDashboard FromFinMind(
        string stockId,
        IReadOnlyList<JsonElement> institutionalRows,
        IReadOnlyList<JsonElement> marginRows,
        IReadOnlyList<JsonElement> holdingRows)
    {
        var institutional = institutionalRows
            .Select(InstitutionalInvestorRow.FromFinMind)
            .Where(row => row is not null)
            .Select(row => row!)
            .OrderByDescending(row => row.Date)
            .Take(15)
            .ToArray();

        var margin = marginRows
            .Select(MarginShortRow.FromFinMind)
            .Where(row => row is not null)
            .Select(row => row!)
            .OrderByDescending(row => row.Date)
            .Take(15)
            .ToArray();

        var holding = holdingRows
            .Select(HoldingShareRow.FromFinMind)
            .Where(row => row is not null)
            .Select(row => row!)
            .OrderByDescending(row => row.Date)
            .ThenBy(row => row.Level)
            .Take(20)
            .ToArray();

        var latestDate = institutional.Select(row => row.Date)
            .Concat(margin.Select(row => row.Date))
            .Concat(holding.Select(row => row.Date))
            .DefaultIfEmpty(string.Empty)
            .Max() ?? string.Empty;

        return new StockChipDashboard(
            stockId,
            latestDate,
            institutional,
            margin,
            holding,
            BuildSignals(institutional, margin, holding));
    }

    private static IReadOnlyList<string> BuildSignals(
        IReadOnlyList<InstitutionalInvestorRow> institutional,
        IReadOnlyList<MarginShortRow> margin,
        IReadOnlyList<HoldingShareRow> holding)
    {
        var signals = new List<string>();

        var latestInstitutional = institutional.FirstOrDefault();
        if (latestInstitutional is not null)
        {
            var direction = latestInstitutional.BuySell > 0 ? "買超" : latestInstitutional.BuySell < 0 ? "賣超" : "持平";
            signals.Add($"最近一筆法人資料為{direction}，數值 {latestInstitutional.BuySell:N0}。");
        }

        var latestMargin = margin.FirstOrDefault();
        if (latestMargin is not null)
        {
            signals.Add($"最近融資餘額 {latestMargin.MarginBalance:N0}，融券餘額 {latestMargin.ShortBalance:N0}。");
        }

        if (holding.Count > 0)
        {
            signals.Add("股權分散資料已取得，可用來觀察持股級距變化。");
        }

        if (signals.Count == 0)
        {
            signals.Add("目前沒有足夠的 FinMind 籌碼資料可以產生摘要。");
        }

        return signals;
    }
}

public sealed record InstitutionalInvestorRow(
    string Date,
    string Name,
    decimal Buy,
    decimal Sell,
    decimal BuySell)
{
    public static InstitutionalInvestorRow? FromFinMind(JsonElement row)
    {
        var date = row.GetString("date");
        if (string.IsNullOrWhiteSpace(date))
        {
            return null;
        }

        return new InstitutionalInvestorRow(
            date,
            row.GetString("name") ?? row.GetString("investor") ?? "法人",
            row.GetDecimal("buy"),
            row.GetDecimal("sell"),
            row.GetDecimal("buy") - row.GetDecimal("sell"));
    }
}

public sealed record MarginShortRow(
    string Date,
    decimal MarginBalance,
    decimal ShortBalance,
    decimal MarginChange,
    decimal ShortChange)
{
    public static MarginShortRow? FromFinMind(JsonElement row)
    {
        var date = row.GetString("date");
        if (string.IsNullOrWhiteSpace(date))
        {
            return null;
        }

        return new MarginShortRow(
            date,
            row.GetDecimal("MarginPurchaseTodayBalance"),
            row.GetDecimal("ShortSaleTodayBalance"),
            row.GetDecimal("MarginPurchaseBuy") - row.GetDecimal("MarginPurchaseSell"),
            row.GetDecimal("ShortSaleBuy") - row.GetDecimal("ShortSaleSell"));
    }
}

public sealed record HoldingShareRow(
    string Date,
    string Level,
    decimal Percent)
{
    public static HoldingShareRow? FromFinMind(JsonElement row)
    {
        var date = row.GetString("date");
        if (string.IsNullOrWhiteSpace(date))
        {
            return null;
        }

        return new HoldingShareRow(
            date,
            row.GetString("HoldingSharesLevel") ?? row.GetString("HoldingShares") ?? "未分類",
            row.GetDecimal("percent"));
    }
}

public static class JsonElementExtensions
{
    public static string? GetString(this JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var property) && property.ValueKind != JsonValueKind.Null
            ? property.GetString()
            : null;
    }

    public static decimal GetDecimal(this JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property))
        {
            return 0;
        }

        return property.ValueKind switch
        {
            JsonValueKind.Number when property.TryGetDecimal(out var value) => value,
            JsonValueKind.String when decimal.TryParse(property.GetString(), out var value) => value,
            _ => 0
        };
    }
}
