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

## Cross-language validation

The OEIE v0.1.0 schema has been tested with two independent
JSON Schema Draft 2020-12 implementations:

- JsonSchema.Net 9.4.1 (.NET 8 / C#)
- AJV 8 with ajv-formats 2 (Node.js / JavaScript)

### Reproducing the JavaScript interoperability tests

Node.js and npm are required only for these optional tests.

From the repository root:

```powershell
cd tests/interoperability
npm ci
npm test
```

The test suite validates all five example records using AJV
with JSON Schema Draft 2020-12.

All five examples must produce their expected validation
results for the test suite to pass.

Both implementations produced the expected results for all five
prototype example records:

- invalid-date.json - rejected
- invalid-incomplete-coordinates.json - rejected
- invalid-unknown-type.json - rejected
- valid-heatwave.json - accepted
- valid-minimal-with-evidence.json - accepted

Tested on 14 October 2026.

These tests demonstrate agreement for the current examples.
They do not constitute comprehensive JSON Schema conformance
testing or certification of OEIE as an interoperability standard.

## Development

OEIE is currently an experimental prototype. Its schema, vocabulary and validation behaviour require further development and review before being presented as a stable exchange standard.

OEIE is released under the MIT licence. See the LICENSE file for details. Formal contribution guidelines are still being developed.

## Community and Support

Open Environmental Impact Exchange (OEIE) is an independent, open-source project. We welcome questions, feedback and collaboration from researchers, conservation organisations, public bodies and software developers worldwide.

- **Questions and support:** Use [GitHub Discussions](https://github.com/RichardP932/OpenEnvironmentalImpactExchange/discussions) to ask questions about OEIE or how to use it.
- **Ideas and collaboration:** Use GitHub Discussions to propose improvements, discuss interoperability with existing environmental data standards or explore potential collaborations.
- **Bug reports:** Use [GitHub Issues](https://github.com/RichardP932/OpenEnvironmentalImpactExchange/issues) to report defects or unexpected behaviour.
- **Code contributions:** Pull requests are welcome. Please open a discussion first for substantial changes to the exchange specification.

OEIE is maintained independently on a part-time basis. Responses and reviews will be provided as time permits.

Our goal is to develop an open, interoperable environmental impact exchange format that can be adopted and improved by organisations and individuals around the world.
