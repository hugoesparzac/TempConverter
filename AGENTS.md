# Project Instructions

## Domain & Goal
Cross-platform desktop temperature converter (Windows, Linux, macOS) built with C#, .NET 10, and Avalonia UI (MVVM) with an isolated `.Core` domain library.

## Stack & Architecture
- **Runtime & SDK:** .NET 10 (`net10.0`), `Nullable=enable`, `ImplicitUsings=enable`.
- **UI Framework:** Avalonia UI 11+ (AXAML with `x:DataType` compiled bindings, `FluentTheme`, Inter font).
- **MVVM & DI:** `CommunityToolkit.Mvvm` (`[ObservableProperty]`, `[RelayCommand]`) + `Microsoft.Extensions.DependencyInjection`.
- **Solution Layout:**
  - `src/TempConverter.Core`: Pure C# class library (temperature scales, conversion formulas, rounding/precision rules, validation). Zero UI or Avalonia dependencies.
  - `src/TempConverter.App`: Avalonia MVVM desktop app (`Views/*.axaml`, `ViewModels`, `App.axaml`, `Program.cs`). References `TempConverter.Core`.
  - `tests/TempConverter.Core.Tests`: xUnit tests (`[Fact]`, `[Theory]`) for conversion accuracy, edge cases (e.g., absolute zero), and validation.
  - `tests/TempConverter.App.Tests`: xUnit + `Avalonia.Headless.XUnit` (`[AvaloniaFact]`) for ViewModels, AXAML bindings, and user interactions.
- **Testing Framework:** Strictly xUnit (`[Fact]`, `[Theory]`, `[AvaloniaFact]`). Never use MSTest or NUnit.

## Code Style & Quality Gates (Enforced by CI & Husky)
- **Strict Build Rules:** `Directory.Build.props` enforces `AnalysisLevel=latest-all`, `EnforceCodeStyleInBuild=true`, and `TreatWarningsAsErrors=true`. Zero compiler or Roslyn analyzer warnings are permitted.
- **C# Conventions (`.editorconfig`):**
  - Use file-scoped namespaces (`namespace TempConverter.Core;`).
  - Always use Allman-style braces (`{` on a new line) and never omit braces on single-line `if`, `else`, `foreach`, or `while` statements.
  - Use explicit types instead of `var` for built-in types (`int`, `double`, `string`) and whenever the type is not immediately apparent on the right-hand side.
  - Prefer modern C# syntax: collection expressions (`[...]`), pattern matching, and `switch` expressions.
  - Prefix private fields with `_camelCase` and mark them `readonly` when never reassigned.
  - With `CommunityToolkit.Mvvm`, prefer `[ObservableProperty]` on `partial` properties rather than fields to avoid source-generator warnings.
- **Verification:** Always run `dotnet format --verify-no-changes` and `dotnet build --configuration Release` before finishing a task.

## Testing Strategy (for `/pair`)
- **Domain & Math (`TempConverter.Core`):** xUnit `[Theory]` with `[InlineData]` covering standard points, negative values, decimals, and physical bounds.
- **Presentation State (`ViewModels`):** xUnit `[Fact]` verifying reactive property updates, unit swapping, and invalid input handling without opening windows.
- **UI & Bindings (`Views/*.axaml`):** xUnit `[AvaloniaFact]` via `Avalonia.Headless.XUnit` verifying `x:DataType` bindings, text input, and scale selection.
- **Native OS Behavior:** Manual verification across Windows, Linux, and macOS (window sizing, font rendering, clipboard).

## Cross-Platform Guardrails
- Use `.WithInterFont()` in `AppBuilder` for consistent typography across Linux, macOS, and Windows.
- Use `CultureInfo.InvariantCulture` when parsing/formatting internal numeric strings while respecting user display formatting cleanly.
- Never hardcode OS-specific paths, fonts, or window chrome assumptions.
- Avalonia 12: `.WithInterFont()` only registers the Inter collection; FluentTheme supplies the Inter-first composite default font family.

## Workflow & Issue Tracking (GitHub CLI)
- Never create `backlog.md` or `plan.md`. All stories and plans live in GitHub Issues via `gh`.
- Work strictly in vertical slices (`Core logic -> ViewModel -> AXAML View -> xUnit Tests` in one issue).
- Branch naming must match `<type>/<kebab-case-description>` (e.g., `feat/kelvin-conversion`, `fix/decimal-precision`).
- Never run `git commit` automatically; propose Conventional Commit messages (`feat:`, `fix:`, `test:`, `refactor:`) with a max 72-character title, a blank second line, and `Closes #<issue>` in the body.

## Commands
- **Run all tests:** `dotnet test`
- **Run Core tests only:** `dotnet test tests/TempConverter.Core.Tests`
- **Run UI headless tests only:** `dotnet test tests/TempConverter.App.Tests`
- **Run desktop app:** `dotnet run --project src/TempConverter.App`
- **Verify formatting & build:** `dotnet format --verify-no-changes && dotnet build --configuration Release`
- **Publish binaries:**
  - Windows: `dotnet publish src/TempConverter.App -c Release -r win-x64 --self-contained -p:PublishSingleFile=true`
  - Linux: `dotnet publish src/TempConverter.App -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true`
  - macOS (Apple Silicon): `dotnet publish src/TempConverter.App -c Release -r osx-arm64 --self-contained`

## Skill Routing (Load automatically when applicable)
- **Process:** `backlog`, `story`, `pair`
- **C# & Architecture:** `csharp-expert`, `csharp-refactoring`
- **Testing (xUnit only):** `code-testing`, `assertion-quality`, `test-anti-patterns`, `test-smell-detection`, `test-gap-analysis`, `coverage-analysis`, `run-tests`

## Project Conventions & Expensive Decisions
<!-- Automatically updated when running /backlog and /pair -->
