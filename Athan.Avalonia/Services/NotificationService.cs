using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DesktopNotifications;
using DesktopNotifications.Apple;
using DesktopNotifications.FreeDesktop;
using DesktopNotifications.Windows;

namespace Athan.Avalonia.Services;

internal sealed class NotificationService
{
    private const int Threshold = 10;

    private bool initialized;

    private readonly List<DateTimeOffset> scheduledNotifications = new List<DateTimeOffset>();

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

    public async Task ScheduleNotificationAsync(string title, string body, TimeSpan timeSpan)
    {
        if (!initialized)
        {
            throw new InvalidOperationException("The notification manager was not initialized.");
        }

        var schedule = DateTimeOffset.Now + timeSpan;

        if (scheduledNotifications.Any(dateTime => (schedule.Ticks - dateTime.Ticks) > Threshold))
        {
            return;
        }

        await manager.ScheduleNotification(new Notification()
        {
            Title = title,
            Body = body
        }, schedule);

        scheduledNotifications.Add(schedule);
    }

    private void OnNotificationActivated(object? sender, NotificationActivatedEventArgs eventArgs)
    {
        var notification =
            scheduledNotifications.FirstOrDefault(date => (date.Ticks - DateTimeOffset.Now.Ticks) > Threshold);

        scheduledNotifications.Remove(notification);
    }
}