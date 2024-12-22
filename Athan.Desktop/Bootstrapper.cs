using System.IO;
using System.Net.Http;
using Athan.Desktop.Features.Offline;
using Athan.Desktop.Features.Prayers;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using DesktopNotifications;
using DesktopNotifications.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Wpf.Ui;
using NavigationService = Athan.Desktop.Features.Shell.NavigationService;

namespace Athan.Desktop;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var collection = new ServiceCollection();

        collection.AddSerilog(configuration =>
        {
            var path = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Athan",
                "log.txt");

            configuration
                .MinimumLevel.Verbose()
                .WriteTo.File(path);
        });

        collection.AddSingleton<SnackbarService>();
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
        collection.AddSingleton<PrayerService>();

        collection.AddSingleton<SettingsView>();
        collection.AddSingleton<SettingsViewModel>();
        collection.AddSingleton<SettingsService>();

        collection.AddSingleton<OfflineView>();
        collection.AddSingleton<OfflineViewModel>();

        return collection.BuildServiceProvider();
    }
}