# GitHub Copilot Instructions

This repository must follow these implementation rules:

## Architecture
- Use **Vertical Slice Architecture**.
- Organize features by slice (for example: `Features/<FeatureName>/<Command|Query|Handler|Endpoint|Validator>`), not by technical layers.
- Keep all behavior for a use case in the same slice (request, validation, handler, and endpoint wiring).
- Prefer high cohesion inside each slice and avoid cross-slice coupling.

## Messaging and Application Flow
- Use **WolverineFx** for command/query handling and asynchronous messaging workflows.
- Prefer Wolverine handlers and message contracts over custom orchestration patterns.
- Use Wolverine middleware/policies for cross-cutting concerns when appropriate.
- Keep message contracts explicit and focused on a single use case.

## API Style
- Use **Minimal APIs** for HTTP endpoints.
- Define endpoints close to their corresponding feature slice.
- Avoid introducing MVC Controllers or Razor Pages patterns for API endpoints.
- API endpoints must implement the **IEndpoint** contract.

## Project Conventions
- Apply these rules consistently in Worker, API, and Web integration points.
- Keep changes minimal, testable, and aligned with existing coding style.
- Do not introduce architecture patterns that conflict with Vertical Slice + WolverineFx + Minimal APIs.
