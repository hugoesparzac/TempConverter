using CommunityToolkit.Mvvm.ComponentModel;

namespace TempConverter.App.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string AppName { get; set; } = "TempConverter";
}
