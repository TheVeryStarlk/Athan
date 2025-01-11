using System.ComponentModel;
using System.Threading.Tasks;
using Athan.Avalonia.Extensions;
using Athan.Avalonia.Features.Prayers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopNotifications;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellViewModel(INotificationManager notificationManager, PrayerViewModel prayerViewModel) : ObservableRecipient
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; } = prayerViewModel;

    [RelayCommand]
    private Task MinimizingAsync()
    {
        return notificationManager.ShowAsync(
            "Running in background",
            "You can open Athan from the tray icon menu.");
    }
}