## Description
<!-- Briefly describe the vertical slice implemented -->

Closes #

## Vertical Slice Checklist
- [ ] Core domain logic implemented in `TempConverter.Core` (zero UI dependencies)
- [ ] ViewModel state and commands implemented in `TempConverter.App`
- [ ] AXAML view updated using compiled bindings (`x:DataType`)
- [ ] Unit tests (`[Fact]` / `[Theory]`) added in `TempConverter.Core.Tests`
- [ ] Headless UI tests (`[AvaloniaFact]`) added in `TempConverter.App.Tests`
- [ ] Verified locally with `dotnet format --verify-no-changes` and `dotnet test`
