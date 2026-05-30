using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

namespace Siraj.Features.Prayers;

internal sealed class NotificationService
{
    public void Show(string title, string message)
    {
        var notification = new AppNotificationBuilder()
            .AddText(title)
            .AddText(message)
            .SetAudioEvent(AppNotificationSoundEvent.Reminder)
            .BuildNotification();

        AppNotificationManager.Default.Show(notification);
    }
}