# Task Plan

## Completed Task: Install Yarn for ClientApp

### Goal

Vendor a project-local Yarn release for `StockChips.Web/ClientApp` so the frontend can be installed and run without depending on a machine-wide Yarn install.

### Acceptance Criteria

- [x] Add a local Yarn release file under `StockChips.Web/ClientApp/.yarn/releases`.
- [x] Add project config so Yarn prefers the vendored release for this app.
- [x] Add a Windows wrapper for local use from the client app folder.
- [x] Verify the vendored Yarn reports its version.
- [x] Update the local dev instructions to match the project-pinned Yarn setup.

### Verification

- [x] `.\yarn.cmd --version` reports `1.22.22` when `NODE_EXE` points at the bundled Node runtime.
- [x] Direct execution of `.yarn/releases/yarn-1.22.22.js` with the bundled Node runtime also reports `1.22.22`.

## Completed Baseline Task: Prepare Markdown Documentation and Publish to GitHub

### Goal

Create a beginner-friendly documentation structure for a Taiwan stock market 籌碼 website so future Codex work has clear product context.

### Acceptance Criteria

- [x] Inspect the example Markdown structure.
- [x] Create root-level project overview docs.
- [x] Create the first domain doc for 籌碼 concepts.
- [x] Record FinMind as the first data source.
- [x] Create the first feature doc for the initial research dashboard.
- [x] Create a task checklist and lessons file.
- [x] Confirm first app stack: ASP.NET Core + Next.js Pages Router.
- [x] Confirm local `.env` token storage for the first version.
- [x] Confirm first market scope: TWSE + TPEx where FinMind supports both.
- [x] Confirm first screen: one-stock dashboard.
- [x] Confirm UI language: Traditional Chinese.
- [x] Confirm the exact FinMind API version and authentication approach: use v4 endpoint with local token.
- [ ] Confirm available FinMind fields for the first datasets.
- [x] Decide whether the first version includes TWSE only or TWSE plus OTC: use TWSE + TPEx where FinMind supports both.
- [x] Initialize the Git repository and connect it to GitHub.
- [x] Exclude the `Example/` folder from version control.
- [x] Commit the current project files and push them to `LeoLovina/Stock`.

### Files Created

- [x] `README.md`
- [x] `AGENTS.md`
- [x] `docs/domains/chips.md`
- [x] `docs/integrations/finmind.md`
- [x] `docs/features/current-feature.md`
- [x] `docs/features/initial-research-dashboard.md`
- [x] `docs/features/registry.json`
- [x] `tasks/todo.md`
- [x] `tasks/lessons.md`
- [x] `StockChips.slnx`
- [x] `StockChips.Web/**`
- [x] `StockChips.Web/ClientApp/.env.local.example`

### Next Steps

- Review `docs/domains/chips.md` and replace assumptions with confirmed decisions.
- Review `docs/integrations/finmind.md` and confirm the API version/token approach.
- Review `docs/features/initial-research-dashboard.md` and narrow the first dashboard scope.
- Choose whether the first implementation uses live FinMind calls or saved sample responses.

### Verification

- [x] `dotnet build StockChips.slnx --no-restore`
- [ ] Frontend install/build was deferred during the documentation baseline because Yarn had not been added yet.
- Publish the documentation-only baseline to GitHub after Git is initialized.
- Verify the first remote sync stays clean after future content updates.
