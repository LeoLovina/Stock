# Taiwan Stock Chips Website

This project documents and will later build a Taiwan stock market "籌碼" website.

The first goal is not to write code yet. The first goal is to describe the product clearly enough that Codex can help build it step by step without guessing important business rules.

## First Data Source

Use FinMind as the first data provider.
The user has a FinMind free account, so the first implementation should be designed around safe token storage, cached API calls, and clear handling of delayed or missing data.

## First Version Stack

- Backend: ASP.NET Core host project in `StockChips.Web`.
- Frontend: Next.js Pages Router app in `StockChips.Web/ClientApp`.
- Local secrets: `StockChips.Web/.env`.
- First market scope: TWSE + TPEx where FinMind supports both.
- First screen: one-stock 籌碼 dashboard.
- UI language: Traditional Chinese.

## Local Configuration

1. Copy `StockChips.Web/.env.example` to `StockChips.Web/.env`.
2. Put your FinMind token in `FINMIND_TOKEN`.
3. Copy `StockChips.Web/ClientApp/.env.local.example` to `StockChips.Web/ClientApp/.env.local`.
4. Set `NEXT_PUBLIC_API_BASE_URL` to the ASP.NET Core URL from `StockChips.Web/Properties/launchSettings.json`.
5. Keep `.env` and `.env.local` local. Do not commit them.

## Local Development

1. Start the backend:

   ```powershell
   dotnet run --project StockChips.Web/StockChips.Web.csproj
   ```

2. Start the frontend in another terminal:

   ```powershell
   cd StockChips.Web/ClientApp
   .\yarn.cmd install
   .\yarn.cmd dev
   ```

3. Open the Next.js URL, usually `http://localhost:3000`.

## What This Website Should Help Users Do

- Search a Taiwan-listed stock by code or name.
- See recent 籌碼 signals in one place.
- Compare investor groups such as foreign investors, investment trusts, dealers, margin trading, short selling, and major holders where data is available.
- Understand whether money flow is accumulating, distributing, or unclear.
- Avoid treating 籌碼 data as investment advice.

## Suggested Documentation Map

- `AGENTS.md`: working rules for Codex in this repo.
- `docs/domains/chips.md`: the business meaning of 籌碼 data.
- `docs/integrations/finmind.md`: FinMind API and dataset notes.
- `docs/features/current-feature.md`: the feature Codex should treat as active.
- `docs/features/initial-research-dashboard.md`: the first planned feature.
- `docs/features/registry.json`: a small index of feature docs.
- `tasks/todo.md`: the active working checklist.
- `tasks/lessons.md`: corrections and lessons that should persist.

## First Practical Workflow

1. Confirm the domain rules in `docs/domains/chips.md`.
2. Review the FinMind integration notes in `docs/integrations/finmind.md`.
3. Confirm the first feature in `docs/features/initial-research-dashboard.md`.
4. Use `tasks/todo.md` as the checklist for the current work.
5. After each major decision, update the related Markdown file before implementing code.
