using System.ComponentModel;
using Athan.Avalonia.Languages;
using Athan.Avalonia.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

// Used for displaying error messages for now
internal sealed partial class DialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private string? message;

    [ObservableProperty]
    private bool isVisible;

    private INotifyPropertyChanged? requester;

    public void Open(INotifyPropertyChanged viewModel)
    {
        requester = viewModel;

        Title = Language.OhNo;

        // Trigger animation
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
        Close();
        WeakReferenceMessenger.Default.Send(new DialogTryAgainRequestMessage(requester!));
    }
}