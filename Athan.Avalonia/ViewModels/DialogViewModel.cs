using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class DialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private string? message;

    [ObservableProperty]
    private bool isVisible;
    
    [RelayCommand]
    private void Close()
    {
        IsVisible = false;
    }
}