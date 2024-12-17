using DesktopNotifications;

namespace Athan.Desktop.Features.Shell;

public class NotificationService(INotificationManager manager)
{
    public event Action? NotificationActivated;

    public async Task InitializeAsync()
    {
        await manager.Initialize();
        manager.NotificationActivated += (_, _) => NotificationActivated?.Invoke();
    }

    public async Task ShowAsync(string title, string description)
    {
        await manager.ShowNotification(new Notification
        {
            Title = title,
            Body = description
        });
    }
}