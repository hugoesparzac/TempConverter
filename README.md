<h1 align="center">TempConverter</h1>

<p align="center">
  Cross-platform desktop temperature converter built with C#, .NET 10, and Avalonia UI using the MVVM pattern.
</p>

<div align="center">

[![Build & Tests](https://img.shields.io/github/actions/workflow/status/hugoesparzac/TempConverter/dotnet.yml?branch=main&style=for-the-badge&label=Build%20%26%20Tests)](https://github.com/hugoesparzac/TempConverter/actions/workflows/dotnet.yml)
<!-- Uncomment once the first GitHub Release is published:
[![Release](https://img.shields.io/github/v/release/hugoesparzac/TempConverter?style=for-the-badge&label=Release)](https://github.com/hugoesparzac/TempConverter/releases/latest)
-->
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=for-the-badge)](CONTRIBUTING.md)

![.NET 10](https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge)
![Avalonia UI](https://img.shields.io/badge/Avalonia_UI-8B44AC?style=for-the-badge)
![Platforms](https://img.shields.io/badge/Platforms-Windows%20%7C%20Linux%20%7C%20macOS-0078D4?style=for-the-badge)

</div>

---

Cross-platform desktop temperature converter built with C#, .NET 10, and Avalonia UI using the MVVM pattern.

## Project Structure

```text
TempConverter/
├── src/
│   ├── TempConverter.App/          # Avalonia UI desktop application (Views & ViewModels)
│   └── TempConverter.Core/         # Core domain models and temperature conversion logic
└── tests/
    ├── TempConverter.App.Tests/    # Headless UI and ViewModel tests (Avalonia.Headless.XUnit)
    └── TempConverter.Core.Tests/   # Unit tests for core conversion logic (xUnit)
```

## Prerequisites

* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (`10.0.404` or newer feature band)

## Getting Started

Restore dependencies, build the solution, and run the test suite:

```bash
dotnet restore
```

```bash
dotnet build --configuration Release
```

```bash
dotnet test --configuration Release
```

Run the desktop application:

```bash
dotnet run --project src/TempConverter.App
```

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for branch naming rules, commit message conventions, and code style guidelines before submitting a pull request.

## License

This project is licensed under the MIT License — see the [LICENSE](https://www.google.com/search?q=LICENSE) file for details.
