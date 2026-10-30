# IRD 2.0 — iRacing Telemetry SDK Reference

## SDK Information

- NuGet package: `SVappsLAB.iRacingTelemetrySDK`
- Installed version: 2.6.0
- Source documentation version: 2.2.0
- Application: IRD 2.0
- Framework: .NET 8

## Important Instructions for AI Coding Agents

This document is the persistent SDK reference for IRD 2.0.

Read the relevant sections before implementing or modifying SDK integration.

The original reference material was written for SDK version 2.2.0. Do not assume all APIs remain identical in version 2.6.0.

The installed NuGet assembly is the final authority for available types, methods, and properties.

When an API has been verified against version 2.6.0, record its exact namespace, signature, and behavior.

Do not repeatedly download SDK source files or research APIs that have already been verified and documented here.

## Verification Status

Use these labels when documenting SDK functionality:

- VERIFIED — Confirmed against the installed 2.6.0 assembly.
- DOCUMENTED — Present in the SDK reference but not yet verified against 2.6.0.
- INCOMPATIBLE — Confirmed to differ from the reference documentation.

---

## SDK Usage and API Reference

Place the existing iRacing Telemetry SDK Advanced SDK Reference content below this heading.

---

## IRD 2.0 Verified API Findings

Record SDK types and signatures confirmed during development.

### Session Information

Verification status: DOCUMENTED

The supplied reference identifies:

- `TelemetrySessionInfo` — parsed session information type
- `client.SessionData` — parsed session information stream
- `client.SessionDataYaml` — raw YAML session stream
- `OnSessionInfoUpdate` — session information callback

Verify these against the installed SDK before implementation.

### Camera Control

Verification status: DOCUMENTED

The supplied reference identifies:

- `client.SimControl.Camera.SwitchToCar(...)`
- `client.SimControl.Camera.SwitchToPosition(...)`
- `client.SimControl.Camera.SetState(...)`

Verify exact argument types and signatures against version 2.6.0.

### Known SDK Compatibility Issues

Record compiler errors, incorrect assumptions, confirmed fixes, and SDK version differences here.

Do not remove resolved issues; mark them as resolved so future development tasks do not repeat the same investigation.