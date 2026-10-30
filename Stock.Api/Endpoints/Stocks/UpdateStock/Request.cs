namespace Stock.Api.Endpoints.Stocks;

public sealed record UpdateStockRequest(string? Symbol, string? Name);
