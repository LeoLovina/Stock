using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using StockChips.Web.Options;

namespace StockChips.Web.ApiClients;

public sealed class FinMindClient
{
    private readonly HttpClient _httpClient;
    private readonly FinMindOptions _options;

    public FinMindClient(HttpClient httpClient, IOptions<FinMindOptions> options)
    {
        _httpClient = httpClient;
        _options = new FinMindOptions
        {
            BaseUrl = Environment.GetEnvironmentVariable("FINMIND_BASE_URL") ?? options.Value.BaseUrl,
            Token = Environment.GetEnvironmentVariable("FINMIND_TOKEN") ?? options.Value.Token
        };
    }

    public async Task<IReadOnlyList<JsonElement>> GetDataAsync(
        string dataset,
        string? stockId,
        DateOnly? startDate,
        CancellationToken cancellationToken)
    {
        var query = new Dictionary<string, string?>
        {
            ["dataset"] = dataset,
            ["stock_id"] = stockId,
            ["start_date"] = startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            ["date"] = startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            ["token"] = string.IsNullOrWhiteSpace(_options.Token) ? null : _options.Token
        };

        var uri = BuildUri(query.Where(pair => !string.IsNullOrWhiteSpace(pair.Value)));
        using var response = await _httpClient.GetAsync(uri, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        if (!document.RootElement.TryGetProperty("data", out var data) || data.ValueKind != JsonValueKind.Array)
        {
            return [];
        }

        return data.EnumerateArray().Select(item => item.Clone()).ToArray();
    }

    private string BuildUri(IEnumerable<KeyValuePair<string, string?>> query)
    {
        var parameters = string.Join("&", query.Select(pair =>
            $"{Uri.EscapeDataString(pair.Key)}={Uri.EscapeDataString(pair.Value!)}"));

        return $"{_options.BaseUrl}?{parameters}";
    }
}
