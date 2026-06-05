# Integration: FinMind

## Decision

Use FinMind as the first data provider for the Taiwan stock 籌碼 website.

The user has a FinMind free account, so the first implementation should support a FinMind API token through local configuration or environment variables. Do not hardcode the token in source code or Markdown.

## Official References

- Main documentation: `https://finmind.github.io/en/`
- API documentation: `https://api.finmindtrade.com/docs`
- Chip data guide: `https://finmind.github.io/v3/tutor/TaiwanMarket/Chip/`

## First Datasets To Use

Start with these FinMind datasets because they match the first dashboard scope:

- `InstitutionalInvestorsBuySell`: per-stock institutional investor buy/sell data.
- `TaiwanStockMarginPurchaseShortSale`: per-stock margin and short-sale data.
- `TaiwanStockHoldingSharesPer`: shareholding distribution by holding level.

Useful later:

- `InstitutionalInvestors`: market-wide institutional investor buy/sell data.
- `Shareholding`: foreign investment and issued-share information.
- `SecuritiesLending`: securities lending transaction details.
- `TaiwanStockPrice`: daily stock price data for chart context.

## Initial API Shape

FinMind examples use:

- Base URL: `https://api.finmindtrade.com/api/v3/data`
- Query parameters:
  - `dataset`
  - `stock_id`
  - `date`
  - token or login-based authentication, depending on the API version and client approach

Before coding, verify whether the implementation should use the v3 endpoint, v4 endpoint, or the official Python SDK.

## Product Rules

- Always show the data date returned by FinMind.
- Treat delayed or missing data as a normal UI state.
- Cache data where practical to avoid unnecessary calls on the free account.
- Keep raw FinMind field names mapped to clear UI labels.
- Never expose the FinMind token in client-side code.

## Open Questions

- Should the app call FinMind directly from a backend API, or should it first use a local import/cache job?
- What is the free-account rate limit for the user's plan?
- Should the first prototype use live FinMind calls or saved sample responses?

