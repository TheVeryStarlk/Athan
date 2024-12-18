using DesktopNotifications;
using Serilog;

namespace Athan.Desktop.Features.Shell;

public class NotificationService(ILogger logger, INotificationManager manager)
{
    public event Action? NotificationActivated;

    public async Task InitializeAsync()
    {
        await manager.Initialize();
        manager.NotificationActivated += (_, _) => NotificationActivated?.Invoke();

        logger.Information("Initialized notification service");
    }

    public async Task ShowAsync(string title, string description)
    {
        logger.Information("Showing notification");

        await manager.ShowNotification(new Notification
        {
            Title = title,
            Body = description
        });
    }
}