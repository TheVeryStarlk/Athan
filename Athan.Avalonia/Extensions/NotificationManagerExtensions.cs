using System.Threading.Tasks;
using DesktopNotifications;

namespace Athan.Avalonia.Extensions;

internal static class NotificationManagerExtensions
{
    public static Task ShowAsync(this INotificationManager notificationManager, string title, string message)
    {
        return notificationManager.ShowNotification(new Notification
        {
            Title = title,
            Body = message
        });
    }
}