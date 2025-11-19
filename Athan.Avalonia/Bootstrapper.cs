using System;
using System.IO;
using System.Net.Http;
using Athan.Avalonia.Features.Prayers;
using Athan.Avalonia.Features.Prayers.Retry;
using Athan.Avalonia.Features.Shell;
using Athan.Services;
using CommunityToolkit.Extensions.DependencyInjection;
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

        ConfigureServices(services);

        return services.BuildServiceProvider();
    }

    [Singleton(typeof(ViewLocator))]
    [Singleton(typeof(HttpClient))]
    [Singleton(typeof(StorageService))]
    [Singleton(typeof(DialogService))]
    [Transient(typeof(LocationService))]
    [Transient(typeof(PrayerService))]
    [Transient(typeof(ShellViewModel))]
    [Transient(typeof(ShellView))]
    [Singleton(typeof(PrayerViewModel))]
    [Singleton(typeof(PrayerView))]
    [Singleton(typeof(RetryViewModel))]
    [Singleton(typeof(RetryView))]
    private static partial void ConfigureServices(IServiceCollection services);
}