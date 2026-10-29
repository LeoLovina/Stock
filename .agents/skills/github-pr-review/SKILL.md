
---
name: github-pr-review
description: Review GitHub Pull Requests for correctness, bugs, security, performance, maintainability, and missing tests. Use when the user asks to review, inspect, analyze, or evaluate a GitHub PR.
---

# GitHub Pull Request Review

## Goal

Perform a careful, evidence-based code review of a GitHub Pull Request.
Identify actionable problems and provide clear recommendations.

## Required inputs

- GitHub PR URL, or repository name and PR number.
- Access to the repository and PR diff.

If the PR URL is missing, ask the user for it.

## Workflow

### 1. Understand the PR

- Read the PR title, description, base branch, and head branch.
- Retrieve the actual diff and changed-file list.
- Read relevant surrounding code and related implementations.
- Identify the intended behavior of the change.

IMPORTANT:
Review the actual diff, not just the PR description.
Do not report existing code as a new change.
Clearly distinguish changed lines from surrounding context.

### 2. Review the code

Check for:

- Incorrect logic and potential bugs.
- Missing null, empty, and boundary handling.
- Security vulnerabilities and authorization issues.
- API contract or backward-compatibility problems.
- Performance and resource-management problems.
- Database, concurrency, and transaction risks.
- Error handling and logging problems.
- Maintainability and unnecessary complexity.

Only report findings supported by code evidence.
Avoid speculative or purely stylistic comments unless they
materially affect maintainability.

### 3. Review tests

- Inspect existing tests and CI/build configuration where relevant.
- Identify missing tests for important behavior and edge cases.
- Run appropriate tests only when the environment permits.
- Clearly state which tests were actually run and their results.
- Never claim that a build or test passed without evidence.

### 4. Validate findings

For every potential finding:

- Confirm that the issue is caused by the PR.
- Identify the affected file and line.
- Explain a realistic failure scenario.
- Explain the impact.
- Suggest a practical fix.

Do not invent line numbers, test results, or repository behavior.

### 5. Assign severity

Use these levels:

- P0: Critical; blocks release or causes catastrophic impact.
- P1: High; serious defect or security issue.
- P2: Medium; meaningful bug or reliability issue.
- P3: Low; minor issue with a concrete benefit to fixing it.

Do not assign severity to suggestions that are not actual defects.

### 6. Produce the review report

Use this format:

# PR Review

## Summary
Briefly explain what the PR actually changes.

## Findings
For each finding, provide:
- Severity
- File and line
- Problem
- Impact or reproduction scenario
- Suggested fix

If no actionable issues are found, explicitly say so.

## Tests
List tests run, their results, and important tests that remain
unverified.

## Recommendation
Choose one:
- Request changes
- Approve
- Comment / needs further verification

Explain the reasoning and mention any remaining uncertainty.

## Safety rules

- Do not modify source code as part of a review unless asked.
- Do not merge the PR.
- Do not submit, approve, or post a GitHub review without
  explicit user authorization.
- A recommendation is not the same as submitting a review.
- Never treat the PR description as proof that a feature was
  implemented in the diff.
- If repository access is unavailable, explain the limitation
  instead of pretending to have inspected the code.
