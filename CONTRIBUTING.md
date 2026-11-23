# Contributing to TempConverter

Thank you for contributing to TempConverter. This repository enforces automated code quality, formatting, and commit history standards both locally (via Husky.Net) and in CI (via GitHub Actions).

## Development Setup

1. Clone the repository and restore local .NET tools (including Husky):

```bash
dotnet tool restore
dotnet husky install
```

2. Build and run tests to verify your environment:

```bash
dotnet build
dotnet test
```

## Branch Naming Convention

All branches must follow `<type>/<kebab-case-description>`:

* Allowed types: `feat`, `fix`, `docs`, `style`, `refactor`, `perf`, `test`, `chore`, `ci`, `hotfix`
* Examples:
* `feat/kelvin-conversion`
* `fix/decimal-rounding-precision`
* `docs/update-setup-instructions`



## Commit Message Standard

This project follows [Conventional Commits](https://www.conventionalcommits.org/):

```text
<type>(<optional-scope>): <short description>

- Optional bullet point explaining what changed
- Another detail if needed
```

* **Allowed types:** `feat`, `fix`, `docs`, `style`, `refactor`, `perf`, `test`, `chore`, `ci`
* **Title limit:** Maximum 72 characters.
* **Blank line rule:** If you include a commit body or bullet points, the second line **must** remain completely empty.

## Code Style & Quality Gates

* **Formatting:** Governed by `.editorconfig` and `.gitattributes` (`LF` line endings). Run `dotnet format` before pushing.
* **Static Analysis:** `AnalysisLevel=latest-all` and `TreatWarningsAsErrors=true` are enabled solution-wide in `Directory.Build.props`. All compiler and analyzer warnings must be resolved before merging.
* **Testing:** All new features and bug fixes must include corresponding unit or headless UI tests using xUnit.
