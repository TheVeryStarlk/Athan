using System.Runtime.InteropServices;
using DesktopNotifications;
using DesktopNotifications.FreeDesktop;
using DesktopNotifications.Windows;

namespace Athan.Avalonia.Services;

internal sealed class NotificationService
{
    public event Action? NotificationActivated;

    private const int Threshold = 10;

    private bool isInitialized;

    private readonly List<DateTime> notifications = new List<DateTime>();

    private readonly INotificationManager? manager;

    public NotificationService()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            manager = new WindowsNotificationManager(
                WindowsApplicationContext.FromCurrentProcess(nameof(Athan)));
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            manager = new FreeDesktopNotificationManager(
                FreeDesktopApplicationContext.FromCurrentProcess(nameof(Athan)));
        }
    }

    public async Task InitializeAsync()
    {
        if (!isInitialized)
        {
            if (manager is not null)
            {
                await manager.Initialize();
            }

            isInitialized = true;
        }
    }

    public void ScheduleNotification(string title, string body, DateTime date)
    {
        if (!isInitialized)
        {
            throw new InvalidOperationException("The notification manager was not initialized.");
        }

        if (notifications.Any(dateTime => (date.Ticks - dateTime.Ticks) < Threshold))
        {
            return;
        }

        var timer = new System.Timers.Timer()
        {
            Interval = date > DateTime.Now
                ? (date - DateTime.Now).TotalMilliseconds
                : (DateTime.Now - date).TotalMilliseconds,

            Enabled = true
        };

        timer.Start();

        timer.Elapsed += async (_, _) =>
        {
            if (manager is null)
            {
                return;
            }

            await manager.ShowNotification(new Notification()
            {
                Title = title,
                Body = body
            });

            HandleNotificationActivated();

            timer.Dispose();
        };

        notifications.Add(date);
    }

    private void HandleNotificationActivated()
    {
        var notification = notifications.FirstOrDefault(date => (date.Ticks - DateTimeOffset.Now.Ticks) < Threshold);
        notifications.Remove(notification);

        NotificationActivated?.Invoke();
    }
}