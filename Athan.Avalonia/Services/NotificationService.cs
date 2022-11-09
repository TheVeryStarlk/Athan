using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using DesktopNotifications;
using DesktopNotifications.Apple;
using DesktopNotifications.FreeDesktop;
using DesktopNotifications.Windows;

namespace Athan.Avalonia.Services;

internal sealed class NotificationService
{
    private const int Threshold = 10;

    private bool initialized;

    private readonly List<DateTime> notifications = new List<DateTime>();

    private readonly INotificationManager manager;

    public NotificationService()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            manager = new WindowsNotificationManager(WindowsApplicationContext.FromCurrentProcess(nameof(Athan)));
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            manager = new FreeDesktopNotificationManager();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            manager = new AppleNotificationManager();
        }
        else
        {
            throw new PlatformNotSupportedException();
        }
    }

    public async Task InitializeAsync()
    {
        if (!initialized)
        {
            await manager.Initialize();
            manager.NotificationActivated += OnNotificationActivated;

            initialized = true;
        }
    }

    public void ScheduleNotification(string title, string body, DateTime date)
    {
        if (!initialized)
        {
            throw new InvalidOperationException("The notification manager was not initialized.");
        }

        if (notifications.Any(dateTime => (date.Ticks - dateTime.Ticks) > Threshold))
        {
            return;
        }

        var timer = new Timer()
        {
            Interval = date > DateTime.Now
                ? (date - DateTime.Now).TotalMilliseconds
                : (DateTime.Now - date).TotalMilliseconds,

            Enabled = true
        };

        timer.Start();

        timer.Elapsed += async (_, _) =>
        {
            await manager.ShowNotification(new Notification()
            {
                Title = title,
                Body = body
            });
        };

        notifications.Add(date);
    }

    private void OnNotificationActivated(object? sender, NotificationActivatedEventArgs eventArgs)
    {
        var notification = notifications.FirstOrDefault(date => (date.Ticks - DateTimeOffset.Now.Ticks) > Threshold);
        notifications.Remove(notification);
    }
}