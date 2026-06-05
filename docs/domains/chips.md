# Domain: Taiwan Stock 籌碼

## Purpose

籌碼 analysis looks at who is buying, selling, holding, borrowing, or lending shares.
For this website, 籌碼 data should help users understand market participation and possible pressure points, not predict prices with certainty.

## Main Concepts

### 三大法人

- Foreign investors: usually called 外資.
- Investment trusts: usually called 投信.
- Dealers: usually called 自營商.

Useful signals may include daily buy/sell amount, consecutive buying or selling days, and changes compared with recent averages.

### 融資融券

- Margin buying: 融資.
- Short selling: 融券.

Useful signals may include balance changes, sharp increases, forced-covering pressure, and whether price movement agrees or disagrees with leverage data.

### 股權分散

This describes how shares are distributed among holders.
Useful signals may include large-holder concentration, retail-holder changes, and whether ownership is becoming more concentrated or more scattered.

### Insider Or Major Holder Data

If available, this may include directors, supervisors, managers, or large shareholders.
This data often changes less frequently than daily trading data.

## Product Rules

- The website should explain data in plain language.
- The website should show source dates so users know how fresh the data is.
- The website should avoid "buy", "sell", or guaranteed prediction language.
- If data is missing, stale, or delayed, the UI should say so clearly.
- Signals should be treated as context, not investment advice.

## Open Questions

- Which data source will be used first?
- Will the first version cover all Taiwan stocks or only listed TWSE stocks?
- Should OTC stocks be included in the first version?
- How many days of history should the first dashboard show?
- Should the UI use Chinese only, or Chinese with English labels?

