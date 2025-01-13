using System.ComponentModel;
using System.Threading.Tasks;
using Athan.Avalonia.Extensions;
using Athan.Avalonia.Features.Prayers;
using CommunityToolkit.Mvvm.ComponentModel;
using DesktopNotifications;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellViewModel(INotificationManager notificationManager, PrayerViewModel prayerViewModel) : ObservableRecipient
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Parent { get; set; } = prayerViewModel;

    public Task MinimizedAsync()
    {
        return notificationManager.ShowAsync(
            "Running in background",
            "You can open Athan from the tray icon menu.");
    }
}