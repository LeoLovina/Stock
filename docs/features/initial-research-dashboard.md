# Feature: Initial Research Dashboard

## Business Summary

Create the first useful screen for a Taiwan stock 籌碼 website.
The user should be able to choose a stock and quickly understand recent chip-related signals without needing to inspect many separate data tables.

The first version should use FinMind as its data provider.

## Target User

A Taiwan stock market retail investor who wants a clearer view of 籌碼 changes before doing deeper research.

## User Goals

- Search for a stock by code or name.
- See the latest available 籌碼 data date.
- Review recent 三大法人 buy/sell behavior.
- Review margin trading and short-selling changes.
- See a plain-language summary of notable changes.
- Know when the website does not have enough data to make a signal meaningful.

## In Scope

- Stock search input.
- Stock summary header.
- Latest data timestamp or trading date.
- 三大法人 daily and recent-trend section.
- 融資融券 daily and recent-trend section.
- 股權分散 section if `TaiwanStockHoldingSharesPer` is available for the selected stock.
- Plain-language signal summary.
- Missing-data and stale-data states.

## Out Of Scope For First Version

- Account login.
- Portfolio tracking.
- Price prediction.
- Trading recommendations.
- Broker order placement.
- Real-time intraday data.
- Paid subscription behavior.

## Acceptance Criteria

- A user can select a stock and see a dashboard for that stock.
- The dashboard identifies the data date used by each section.
- The dashboard uses FinMind data or clearly marked FinMind sample responses.
- The dashboard separates facts from interpretation.
- Missing or stale data is visible and understandable.
- No text tells users to buy or sell a stock.

## Assumptions

- The first version can use delayed or end-of-day data.
- The first dashboard can start with a limited number of chip indicators.
- The first implementation can use cached FinMind responses while the UI is being shaped.
- More advanced features can be added after the basic dashboard is clear.

## Open Questions

- Which exact fields are available for 三大法人?
- Which exact fields are available for 融資融券?
- Should charts be required in the first version, or are tables and summaries enough?
