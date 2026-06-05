# Task Plan

## Current Task: Prepare Markdown Documentation and Publish to GitHub

### Goal

Create a beginner-friendly documentation structure for a Taiwan stock market 籌碼 website so future Codex work has clear product context.

### Acceptance Criteria

- [x] Inspect the example Markdown structure.
- [x] Create root-level project overview docs.
- [x] Create the first domain doc for 籌碼 concepts.
- [x] Record FinMind as the first data source.
- [x] Create the first feature doc for the initial research dashboard.
- [x] Create a task checklist and lessons file.
- [ ] Confirm the exact FinMind API version and authentication approach.
- [ ] Confirm available FinMind fields for the first datasets.
- [ ] Decide whether the first version includes TWSE only or TWSE plus OTC.
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

### Next Steps

- Review `docs/domains/chips.md` and replace assumptions with confirmed decisions.
- Review `docs/integrations/finmind.md` and confirm the API version/token approach.
- Review `docs/features/initial-research-dashboard.md` and narrow the first dashboard scope.
- Choose whether the first implementation uses live FinMind calls or saved sample responses.
- Publish the documentation-only baseline to GitHub after Git is initialized.
- Verify the first remote sync stays clean after future content updates.
