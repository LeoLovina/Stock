namespace StockChips.Web.Options;

public sealed class FinMindOptions
{
    public const string SectionName = "FinMind";

    public string BaseUrl { get; init; } = "https://api.finmindtrade.com/api/v4/data";

    public string? Token { get; init; }
}
