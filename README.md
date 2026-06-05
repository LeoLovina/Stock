# Taiwan Stock Chips Website

This project documents and will later build a Taiwan stock market "籌碼" website.

The first goal is not to write code yet. The first goal is to describe the product clearly enough that Codex can help build it step by step without guessing important business rules.

## What This Website Should Help Users Do

- Search a Taiwan-listed stock by code or name.
- See recent 籌碼 signals in one place.
- Compare investor groups such as foreign investors, investment trusts, dealers, margin trading, short selling, and major holders where data is available.
- Understand whether money flow is accumulating, distributing, or unclear.
- Avoid treating 籌碼 data as investment advice.

## Suggested Documentation Map

- `AGENTS.md`: working rules for Codex in this repo.
- `docs/domains/chips.md`: the business meaning of 籌碼 data.
- `docs/features/current-feature.md`: the feature Codex should treat as active.
- `docs/features/initial-research-dashboard.md`: the first planned feature.
- `docs/features/registry.json`: a small index of feature docs.
- `tasks/todo.md`: the active working checklist.
- `tasks/lessons.md`: corrections and lessons that should persist.

## First Practical Workflow

1. Confirm the domain rules in `docs/domains/chips.md`.
2. Confirm the first feature in `docs/features/initial-research-dashboard.md`.
3. Use `tasks/todo.md` as the checklist for the current work.
4. After each major decision, update the related Markdown file before implementing code.

