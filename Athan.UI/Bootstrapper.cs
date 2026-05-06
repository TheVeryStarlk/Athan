using System;
using Athan.UI.Features.Locations;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Shell;
using Athan.UI.Features.Tasbih;
using Athan.UI.Features.Welcome;
using CommunityToolkit.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static partial class Bootstrapper
{
    public static ServiceProvider Services { get; } = Build();

    private static ServiceProvider Build()
    {
        var services = new ServiceCollection();

        services.AddHttpClient();

#if DEBUG
        services.AddSingleton<TimeProvider, DebugTimeProvider>();
#else
        services.AddSingleton(TimeProvider.System);
#endif

        Configure(services);
        ConfigureViews(services);
        ConfigureViewModels(services);

        return services.BuildServiceProvider();
    }

    [Singleton(typeof(NavigationService), typeof(INavigationService))]
    [Singleton(typeof(SettingsService))]
    [Transient(typeof(GeopositionService))]
    [Transient(typeof(LocationService))]
    [Transient(typeof(DialogService))]
    private static partial void Configure(IServiceCollection services);

    [Singleton(typeof(ShellView))]
    private static partial void ConfigureViews(IServiceCollection services);

    [Singleton(typeof(PrayersViewModelFactory))]
    [Transient(typeof(ShellViewModel))]
    [Transient(typeof(WelcomeViewModel))]
    [Transient(typeof(TasbihViewModel))]
    [Transient(typeof(SettingsViewModel))]
    private static partial void ConfigureViewModels(IServiceCollection services);
}

internal sealed class DebugTimeProvider : TimeProvider
{
    public override DateTimeOffset GetUtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}