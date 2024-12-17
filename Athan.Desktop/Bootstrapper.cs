using System.Net.Http;
using Athan.Desktop.Features.Offline;
using Athan.Desktop.Features.Prayers;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using DesktopNotifications;
using DesktopNotifications.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Desktop;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<Wpf.Ui.SnackbarService>();
        collection.AddSingleton<HttpClient>();

        collection.AddTransient<ShellView>();
        collection.AddTransient<ShellViewModel>();
        collection.AddSingleton<NavigationService>();
        collection.AddSingleton<NotificationService>();

        collection.AddTransient<INotificationManager, WindowsNotificationManager>(static _ =>
        {
            var context = WindowsApplicationContext.FromCurrentProcess(nameof(Athan));
            return new WindowsNotificationManager(context);
        });

        collection.AddSingleton<WelcomeView>();
        collection.AddSingleton<WelcomeViewModel>();
        collection.AddTransient<LocationService>();

        collection.AddSingleton<PrayersView>();
        collection.AddSingleton<PrayersViewModel>();

        collection.AddSingleton<SettingsView>();
        collection.AddSingleton<SettingsViewModel>();
        collection.AddSingleton<SettingsService>();

        collection.AddSingleton<OfflineView>();
        collection.AddSingleton<OfflineViewModel>();

        return collection.BuildServiceProvider();
    }
}