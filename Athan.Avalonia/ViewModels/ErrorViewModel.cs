using Athan.Avalonia.Models;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ErrorViewModel : ObservableRecipient, IRecipient<Error>
{
    [ObservableProperty]
    public partial string? Message { get; set; }

    public ErrorViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    [RelayCommand]
    private void Close()
    {
        var lifetime = (ClassicDesktopStyleApplicationLifetime) Application.Current!.ApplicationLifetime!;
        lifetime.Shutdown(1);
    }

    public void Receive(Error error)
    {
        Message = error.Message;
    }
}