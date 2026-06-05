import { FormEvent, useState } from 'react';
import { useStockDashboard, useStockSearch } from '@/hooks/useStockDashboard';

export function StockDashboard() {
  const [query, setQuery] = useState('2330');
  const [selectedStockId, setSelectedStockId] = useState('2330');
  const search = useStockSearch(query);
  const dashboard = useStockDashboard(selectedStockId);

  function submitSearch(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const firstMatch = search.data?.[0];
    setSelectedStockId(firstMatch?.stockId ?? query.trim());
  }

  return (
    <div className="dashboard">
      <section className="hero">
        <div>
          <p className="eyebrow">第一版</p>
          <h2>輸入股票代號，快速查看最近籌碼變化。</h2>
          <p className="muted">資料由後端安全呼叫 FinMind，前端不會接觸 API token。</p>
        </div>
        <form className="search-panel" onSubmit={submitSearch}>
          <label htmlFor="stock-query">股票代號或名稱</label>
          <div className="search-row">
            <input
              id="stock-query"
              value={query}
              onChange={(event) => setQuery(event.target.value)}
              placeholder="例如 2330 或 台積電"
            />
            <button type="submit">查詢</button>
          </div>
          {search.data && search.data.length > 0 ? (
            <div className="quick-results">
              {search.data.slice(0, 5).map((stock) => (
                <button
                  key={stock.stockId}
                  type="button"
                  onClick={() => {
                    setQuery(stock.stockId);
                    setSelectedStockId(stock.stockId);
                  }}
                >
                  {stock.stockId} {stock.stockName}
                </button>
              ))}
            </div>
          ) : null}
        </form>
      </section>

      {dashboard.isLoading ? <p className="panel">載入 FinMind 籌碼資料中...</p> : null}
      {dashboard.isError ? <p className="panel error">無法取得資料，請確認後端已啟動且 FINMIND_TOKEN 已設定。</p> : null}

      {dashboard.data ? (
        <>
          <section className="summary-band">
            <div>
              <p className="eyebrow">目前股票</p>
              <h2>{dashboard.data.stockId}</h2>
            </div>
            <div>
              <p className="eyebrow">最新資料日</p>
              <h2>{dashboard.data.latestDataDate || '尚無資料'}</h2>
            </div>
          </section>

          <section className="grid">
            <article className="panel">
              <h3>摘要訊號</h3>
              <ul className="signal-list">
                {dashboard.data.signals.map((signal) => (
                  <li key={signal}>{signal}</li>
                ))}
              </ul>
            </article>

            <article className="panel">
              <h3>三大法人</h3>
              <table>
                <thead>
                  <tr>
                    <th>日期</th>
                    <th>法人</th>
                    <th>買賣超</th>
                  </tr>
                </thead>
                <tbody>
                  {dashboard.data.institutionalInvestors.map((row) => (
                    <tr key={`${row.date}-${row.name}`}>
                      <td>{row.date}</td>
                      <td>{row.name}</td>
                      <td>{formatNumber(row.buySell)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </article>

            <article className="panel">
              <h3>融資融券</h3>
              <table>
                <thead>
                  <tr>
                    <th>日期</th>
                    <th>融資餘額</th>
                    <th>融券餘額</th>
                  </tr>
                </thead>
                <tbody>
                  {dashboard.data.marginShort.map((row) => (
                    <tr key={row.date}>
                      <td>{row.date}</td>
                      <td>{formatNumber(row.marginBalance)}</td>
                      <td>{formatNumber(row.shortBalance)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </article>

            <article className="panel">
              <h3>股權分散</h3>
              <table>
                <thead>
                  <tr>
                    <th>日期</th>
                    <th>級距</th>
                    <th>比例</th>
                  </tr>
                </thead>
                <tbody>
                  {dashboard.data.holdingShares.map((row) => (
                    <tr key={`${row.date}-${row.level}`}>
                      <td>{row.date}</td>
                      <td>{row.level}</td>
                      <td>{row.percent.toFixed(2)}%</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </article>
          </section>
        </>
      ) : null}
    </div>
  );
}

function formatNumber(value: number) {
  return new Intl.NumberFormat('zh-TW').format(value);
}
