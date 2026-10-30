# IRD 2.0 — AI Development Instructions

## Project Overview

IRD 2.0 is a modernization of iRacing Replay Director.

The original application and existing fork modifications must be preserved. All new development should remain within the `ird2` directory unless explicitly authorized.

The application will support iRacing telemetry, automated camera selection, incident detection, replay management, and OBS integration.

## Project Structure

- `IRD2.Core/` — Core application logic and iRacing integration
- `IRD2.Tests/` — Tests for IRD 2.0
- `IRD2.UI/` — User interface
- `iRacingReplayDirector.Core/` — Existing shared or migrated components; inspect before modifying
- `docs/` — Persistent project documentation

Inspect the actual solution and project files before assuming dependencies or build commands.

## iRacing SDK

IRD 2.0 uses `SVappsLab.iRacingTelemetrySDK` version 2.6.0 through NuGet.

This project is an SDK consumer, not the SDK source repository.

### Reference priority

1. `docs/IRACING_SDK.md` — Locally verified SDK reference
2. Installed NuGet package version 2.6.0
3. Version-matched SDK source documentation, when local information is insufficient

Do not repeatedly fetch GitHub documentation for SDK APIs that have already been verified locally.

Never invent SDK types, properties, methods, or events.

When SDK documentation and the installed assembly disagree, the installed assembly takes precedence.

Record verified discoveries and compatibility issues in `docs/IRACING_SDK.md`.

## Development Workflow

Before beginning a task:

1. Read `docs/IRD_PROGRESS.md` if it exists.
2. Inspect the relevant existing source files.
3. Identify the smallest change necessary.
4. Avoid unrelated architectural changes.

When editing:

- Prefer targeted edits over complete file rewrites.
- Preserve existing functionality.
- Do not repeatedly attempt the same unsuccessful edit.
- After two failed editing attempts, diagnose the editing mechanism before continuing.
- Verify file contents after editing.

## Build and Validation

Build the affected project using the appropriate .NET tooling.

Use compiler output to identify errors rather than guessing at SDK APIs.

Run relevant tests when available.

Do not claim that a change works unless the appropriate build or test has succeeded.

## Context Management

Conversation history is not the authoritative record of development progress.

Use the repository source files and persistent documentation as the source of truth.

At the completion of each development milestone, update `docs/IRD_PROGRESS.md` with:

- Completed changes
- Build and test results
- Outstanding problems
- Failed approaches that should not be repeated
- Next recommended task

Keep development tasks focused enough to minimize context compaction.

## Development Boundaries

Do not modify the original iRacing Replay Director application unless explicitly instructed.

Do not rewrite large portions of IRD 2.0 merely to resolve isolated compiler errors.

Do not replace existing dependencies or redesign architecture without first explaining the reason and obtaining approval.