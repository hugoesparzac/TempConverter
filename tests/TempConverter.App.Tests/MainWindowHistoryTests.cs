using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;
using TempConverter.Core;

namespace TempConverter.App.Tests;

public class MainWindowHistoryTests
{
    [AvaloniaFact]
    public void MainWindow_HasHistoryHeadingAndClearButton()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBlock? heading = window.FindControl<TextBlock>("HistoryHeading");
        Assert.NotNull(heading);
        Assert.Equal("History", heading.Text);

        Button? clearButton = window.FindControl<Button>("ClearHistoryButton");
        Assert.NotNull(clearButton);
        Assert.Equal("Clear", clearButton.Content);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_HistoryListBoundToEntries_NewestFirst()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        ItemsControl? historyList = window.FindControl<ItemsControl>("HistoryList");
        Assert.NotNull(historyList);
        Assert.Same(viewModel.History, historyList.ItemsSource);

        viewModel.Input = "37";
        viewModel.CommitConversionCommand.Execute(null);
        viewModel.TargetScale = TemperatureScale.Kelvin;
        viewModel.CommitConversionCommand.Execute(null);
        window.UpdateLayout();

        string[] itemTexts = historyList.GetVisualDescendants()
            .OfType<TextBlock>()
            .Select(block => block.Text!)
            .ToArray();
        Assert.Equal(["37 °C → 310.15 K", "37 °C → 98.6 °F"], itemTexts);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_ClearButtonWiredToClearHistoryCommand_EmptiesList()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        Button? clearButton = window.FindControl<Button>("ClearHistoryButton");
        ItemsControl? historyList = window.FindControl<ItemsControl>("HistoryList");
        Assert.NotNull(clearButton);
        Assert.NotNull(historyList);
        Assert.Same(viewModel.ClearHistoryCommand, clearButton.Command);

        viewModel.Input = "37";
        viewModel.CommitConversionCommand.Execute(null);
        window.UpdateLayout();
        Assert.Single(historyList.GetVisualDescendants().OfType<TextBlock>());

        clearButton.Command!.Execute(null);
        window.UpdateLayout();

        Assert.Empty(viewModel.History);
        Assert.Empty(historyList.GetVisualDescendants().OfType<TextBlock>());

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_ValueInputBindsEnterKeyToCommitConversionCommand()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        KeyBinding? enterBinding = valueInput.KeyBindings
            .OfType<KeyBinding>()
            .SingleOrDefault(binding => binding.Gesture?.Key == Key.Enter);
        Assert.NotNull(enterBinding);
        Assert.Same(viewModel.CommitConversionCommand, enterBinding.Command);

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_ValueInputLostFocus_CommitsValidConversion()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        Assert.NotNull(valueInput);

        viewModel.Input = "37";
        valueInput.RaiseEvent(new FocusChangedEventArgs(Control.LostFocusEvent)
        {
            Source = valueInput,
            OldFocusedElement = valueInput
        });
        window.UpdateLayout();

        Assert.Single(viewModel.History);
        Assert.Equal("37 °C → 98.6 °F", viewModel.History[0].Display);

        window.Close();
    }
}
