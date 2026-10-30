using System.Collections.Concurrent;
using Stock.Api.Models;

namespace Stock.Api.Data;

public sealed class StockStore
{
    private readonly ConcurrentDictionary<string, StockRecord> _stocks = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyCollection<StockRecord> GetAll()
    {
        return _stocks.Values.OrderBy(stock => stock.Symbol).ToArray();
    }

    public StockRecord? Get(string id)
    {
        return _stocks.GetValueOrDefault(id);
    }

    public StockRecord Create(string symbol, string name)
    {
        var stock = new StockRecord(Guid.NewGuid().ToString("N"), symbol.Trim(), name.Trim());
        _stocks[stock.Id] = stock;
        return stock;
    }

    public StockRecord? Update(string id, string symbol, string name)
    {
        while (_stocks.TryGetValue(id, out var current))
        {
            var updated = current with { Symbol = symbol.Trim(), Name = name.Trim() };
            if (_stocks.TryUpdate(id, updated, current))
            {
                return updated;
            }
        }

        return null;
    }

    public bool Delete(string id)
    {
        return _stocks.TryRemove(id, out _);
    }
}
