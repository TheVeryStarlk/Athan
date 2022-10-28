using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DesktopNotifications;
using DesktopNotifications.Apple;
using DesktopNotifications.FreeDesktop;
using DesktopNotifications.Windows;

namespace Athan.Avalonia.Services;

internal sealed class NotificationService
{
    private bool initialized;

    private readonly INotificationManager manager;

    public NotificationService()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            manager = new WindowsNotificationManager();
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
            initialized = true;
        }
    }

    public async Task ScheduleNotificationAsync(string title, string body, TimeSpan timeSpan)
    {        
        if (!initialized)
        {
            throw new InvalidOperationException("The notification manager was not initialized.");
        }

        await manager.ScheduleNotification(new Notification()
        {
            Title = title,
            Body = body
        }, DateTimeOffset.Now + timeSpan);
    }
}