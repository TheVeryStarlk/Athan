using System.Threading.Tasks;
using Athan.Avalonia.Features.Shell;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.Features.Prayers.Retry;

internal sealed partial class RetryViewModel(DialogService dialogService) : ObservableRecipient
{
    [ObservableProperty]
    public partial string? Message { get; set; }

    private TaskCompletionSource? completionSource;

    public async Task ShowAsync(string message)
    {
        Message = message;
        completionSource = new TaskCompletionSource();

        await dialogService.ShowAsync(this);
        await completionSource.Task;
    }

    [RelayCommand]
    private void Retry()
    {
        dialogService.Close();
        completionSource?.SetResult();
    }

    [RelayCommand]
    private void Close()
    {
        var desktop = (IClassicDesktopStyleApplicationLifetime) Application.Current!.ApplicationLifetime!;
        desktop.Shutdown();
    }
}