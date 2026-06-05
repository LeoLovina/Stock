# Codex Workflow

Use this file as the working agreement for Codex in this project.

## Core Rules

- Read `AGENTS.md` first before making changes.
- Read `tasks/lessons.md` before editing.
- Keep the documentation simple, practical, and tied to the website being built.
- Do not invent financial advice or trading recommendations.
- Clearly separate confirmed facts, assumptions, and open questions.
- For non-trivial work, update `tasks/todo.md` before implementation.

## Context Loading Order

For product or feature work, read files in this order:

1. `AGENTS.md`
2. `tasks/lessons.md`
3. `docs/domains/chips.md`
4. `docs/integrations/finmind.md`
5. `docs/features/current-feature.md`
6. the active feature file listed in `current-feature.md`
7. `tasks/todo.md`

## Documentation Rules

- Put durable business meaning in `docs/domains/`.
- Put external API decisions in `docs/integrations/`.
- Put feature-specific scope and acceptance criteria in `docs/features/`.
- Put the current work plan and progress in `tasks/todo.md`.
- Put recurring corrections in `tasks/lessons.md`.
- If a feature changes the meaning of 籌碼, update the domain doc.
- If a feature changes FinMind usage, update the integration doc.
- If a feature changes what users can do, update the feature doc.

## Definition Of Done

Before calling work complete:

- The active feature doc matches what was built or decided.
- Domain assumptions are still accurate.
- `tasks/todo.md` records what changed and how it was checked.
- Any blocked verification is written down honestly.
