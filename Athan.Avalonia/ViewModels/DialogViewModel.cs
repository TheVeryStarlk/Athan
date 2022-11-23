using System.ComponentModel;
using Athan.Avalonia.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class DialogViewModel : ObservableObject
{
    public INotifyPropertyChanged? Requester { get; private set; }

    [ObservableProperty]
    private string? title = "Oh no!";

    [ObservableProperty]
    private string? message;

    [ObservableProperty]
    private bool isVisible;

    public void Open(INotifyPropertyChanged requester)
    {
        Requester = requester;

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
        WeakReferenceMessenger.Default.Send(new DialogTryAgainRequestMessage(Requester!));
    }
}