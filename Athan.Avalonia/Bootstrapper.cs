using System;
using System.IO;
using System.Net.Http;
using Athan.Avalonia.Features.Prayers;
using Athan.Avalonia.Features.Shell;
using Athan.Services;
using CommunityToolkit.Extensions.DependencyInjection;
using DesktopNotifications;
using DesktopNotifications.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Athan.Avalonia;

internal static partial class Bootstrapper
{
    public static IServiceProvider Build()
    {
        var services = new ServiceCollection();

        services.AddSerilog(configuration =>
        {
            var path = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Athan.log");

            configuration
                .MinimumLevel.Verbose()
                .WriteTo.File(path);
        });

        services.AddSingleton<INotificationManager, WindowsNotificationManager>(static _ =>
        {
            var context = WindowsApplicationContext.FromCurrentProcess(nameof(Athan));
            return new WindowsNotificationManager(context);
        });

        ConfigureServices(services);

        return services.BuildServiceProvider();
    }

    [Singleton(typeof(ViewLocator))]
    [Singleton(typeof(HttpClient))]
    [Singleton(typeof(StorageService))]
    [Transient(typeof(LocationService))]
    [Transient(typeof(PrayerService))]
    [Transient(typeof(ShellViewModel))]
    [Transient(typeof(ShellView))]
    [Singleton(typeof(PrayerViewModel))]
    [Singleton(typeof(PrayerView))]
    private static partial void ConfigureServices(IServiceCollection services);
}