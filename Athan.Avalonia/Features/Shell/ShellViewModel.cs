using Athan.Avalonia.Features.Prayers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using System.ComponentModel;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellViewModel(PrayerViewModel prayerViewModel) : ObservableRecipient
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Parent { get; set; } = prayerViewModel;

    public void Minimized()
    {
        var notification = new AppNotificationBuilder()
            .AddText("Running in background")
            .AddText("You can open Athan from the tray icon menu")
            .BuildNotification();

        AppNotificationManager.Default.Show(notification);
    }
}