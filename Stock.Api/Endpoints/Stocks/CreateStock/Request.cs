namespace Stock.Api.Endpoints.Stocks;

public sealed record CreateStockRequest(string? Symbol, string? Name);
