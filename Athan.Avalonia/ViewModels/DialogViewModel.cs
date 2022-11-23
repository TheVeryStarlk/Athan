using Athan.Avalonia.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class DialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private string? message;

    [ObservableProperty]
    private bool isVisible;

    public void Open()
    {
        IsVisible = false;
        IsVisible = true;
    }

    [RelayCommand]
    private void Close()
    {
        IsVisible = false;
    }

    [RelayCommand]
    private void TryAgain()
    {
        WeakReferenceMessenger.Default.Send<DialogTryAgainRequestMessage>();
    }
}