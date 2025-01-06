using System;
using System.Net.Http;
using Athan.Avalonia.Services;
using Athan.Avalonia.ViewModels;
using Athan.Avalonia.Views;
using Athan.Services;
using CommunityToolkit.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal static partial class Bootstrapper
{
    public static IServiceProvider Build()
    {
        var services = new ServiceCollection();

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