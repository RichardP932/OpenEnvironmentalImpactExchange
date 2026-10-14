# Open Environmental Impact Exchange (OEIE)

An open, platform-independent format for exchanging structured environmental impact observations and supporting evidence.

## Purpose

OEIE provides a common structure for recording and exchanging information about environmental changes, ecological effects, supporting evidence and possible relationships to climate change.

The format distinguishes between observations and their interpretation.

**An observation of environmental change does not, by itself, establish that climate change caused it.**

OEIE is being developed as a standalone open-source project, independently of the Climate Intelligence Platform (CIP).

## Current status

Version 0.1.0 — early prototype.

The repository currently contains:

- A JSON Schema defining the OEIE record structure.
- A .NET 8 command-line validator using JsonSchema.Net.
- Five example records demonstrating valid and invalid data.
- Validation diagnostics and process exit codes.

The specification is experimental and may change.

## Requirements

- .NET 8 SDK

## Build

From the repository root:

```powershell
dotnet build .\OpenEnvironmentalImpactExchange.sln
```

## Validate a record

```powershell
dotnet run --project .\OpenEnvironmentalImpactExchange.Validator -- .\OpenEnvironmentalImpactExchange.Validator\examples\valid-heatwave.json
```

To run all bundled examples:

```powershell
dotnet run --project .\OpenEnvironmentalImpactExchange.Validator
```

## Exit codes

| Code | Meaning |
|---|---|
| 0 | All supplied records passed validation |
| 1 | One or more records failed schema validation |
| 2 | Input file missing or malformed JSON |

## What validation establishes

The validator checks whether a JSON record conforms to the OEIE schema.

It does not independently establish:

- Whether an environmental observation is factually accurate.
- Whether supporting evidence is reliable.
- Whether climate change caused an observed event.
- Whether a reported ecological effect has been scientifically verified.

These distinctions are fundamental to the project.

## Project principles

1. Separate observations from causal interpretations.
2. Preserve uncertainty and the absence of assessment.
3. Support evidence-based environmental reporting.
4. Avoid dependence on a particular cloud provider or application.
5. Enable independent implementations and interoperability.

## Development

OEIE is currently an experimental prototype. Its schema, vocabulary and validation behaviour require further development and review before being presented as a stable exchange standard.

Licensing and contribution arrangements have not yet been finalised.