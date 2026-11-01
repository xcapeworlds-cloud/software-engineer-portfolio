# Progression Engine — runnable .NET 8 project

A small, standalone C# project demonstrating deterministic progression logic, input validation, checked arithmetic, a command-line interface, and automated assertions. Inspired by the reward and upgrade systems used in multiplayer games; **not** extracted from the Sugar.io Unity source.

## Requirements

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

## Run

From this directory:

```sh
dotnet run --project ProgressionEngine.Cli
```

Example input: distance `100`, rebirths `1`, upgrade level `2`. Expected reward: `250` coins. Next upgrade: `225` coins.

## Test

```sh
dotnet run --project ProgressionEngine.Tests
```

Tests are a dependency-free console test harness with assertions and a nonzero exit code on failure; they do not use xUnit/NUnit/MSTest. GitHub Actions runs them on pushes and pull requests.

## Structure

- `ProgressionEngine.Core`: pure business rules with no Unity dependency
- `ProgressionEngine.Cli`: interactive console program
- `ProgressionEngine.Tests`: automated boundary and calculation checks

## Design decisions

- Decimal math for money-like values avoids typical binary floating-point rounding artifacts.
- Inputs are validated explicitly; overflow is checked.
- Pure functions make the rules easy to test without a game engine.
- The demo intentionally has no database, HTTP API, persistence, or authentication. It does not claim production deployment.

## Limitations

Simple progression rules only; no save/load, game state, concurrent updates, or production-grade test framework. Local compilation and test execution should be verified with the .NET SDK.
