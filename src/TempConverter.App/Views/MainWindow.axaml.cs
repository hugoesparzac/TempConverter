using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using TempConverter.App.ViewModels;

namespace TempConverter.App.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ValueInput.LostFocus += OnValueInputLostFocus;
        AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel, handledEventsToo: true);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (ValueInput.IsFocused && e.Source is not Control { Focusable: true })
        {
            _ = TopLevel.GetTopLevel(this)?.FocusManager.Focus(null);
        }
    }

    private void OnValueInputLostFocus(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.CommitConversionCommand.Execute(null);
        }
    }
}
