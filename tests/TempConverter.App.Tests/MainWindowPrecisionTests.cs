using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.VisualTree;
using TempConverter.App.ViewModels;
using TempConverter.App.Views;

namespace TempConverter.App.Tests;

public class MainWindowPrecisionTests
{
    [AvaloniaFact]
    public void MainWindow_HasDecimalPlacesStepperWithDefaultTwo()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        window.Show();

        TextBlock? stepperLabel = window.FindControl<TextBlock>("PrecisionStepperLabel");
        Assert.NotNull(stepperLabel);
        Assert.Equal("Decimal places", stepperLabel.Text);

        TextBlock? stepperValue = window.FindControl<TextBlock>("PrecisionStepperValue");
        Assert.NotNull(stepperValue);
        Assert.Equal("2", stepperValue.Text);

        Assert.NotNull(window.FindControl<RepeatButton>("DecreasePrecisionButton"));
        Assert.NotNull(window.FindControl<RepeatButton>("IncreasePrecisionButton"));

        window.Close();
    }

    [AvaloniaFact]
    public void MainWindow_WhenPrecisionChanges_StepperResultAndFormulaUpdateLive()
    {
        var viewModel = new MainViewModel();
        var window = new MainWindow
        {
            DataContext = viewModel
        };

        window.Show();

        TextBox? valueInput = window.FindControl<TextBox>("ValueInput");
        TextBlock? stepperValue = window.FindControl<TextBlock>("PrecisionStepperValue");
        TextBlock? formulaBlock = window.FindControl<TextBlock>("FormulaText");
        RepeatButton? decreaseButton = window.FindControl<RepeatButton>("DecreasePrecisionButton");
        RepeatButton? increaseButton = window.FindControl<RepeatButton>("IncreasePrecisionButton");
        Assert.NotNull(valueInput);
        Assert.NotNull(stepperValue);
        Assert.NotNull(formulaBlock);
        Assert.NotNull(decreaseButton);
        Assert.NotNull(increaseButton);

        Assert.Same(viewModel.DecreasePrecisionCommand, decreaseButton.Command);
        Assert.Same(viewModel.IncreasePrecisionCommand, increaseButton.Command);

        valueInput.Text = "37";
        window.UpdateLayout();

        TextBlock resultBlock = window.GetVisualDescendants()
            .OfType<TextBlock>()
            .Single(block => block.Name == "ResultText");
        Assert.Equal("2", stepperValue.Text);
        Assert.Equal("98.6", resultBlock.Text);
        Assert.Equal("°F = 37 × 9/5 + 32 = 98.6", formulaBlock.Text);

        viewModel.Precision = 0;
        window.UpdateLayout();

        Assert.Equal("0", stepperValue.Text);
        Assert.Equal("99", resultBlock.Text);
        Assert.Equal("°F = 37 × 9/5 + 32 = 99", formulaBlock.Text);

        window.Close();
    }
}
