using CommunityToolkit.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static partial class Bootstrapper
{
    public static ServiceProvider Build()
    {
        var services = new ServiceCollection();

        Configure(services);
        ConfigureViews(services);
        ConfigureViewModels(services);

        return services.BuildServiceProvider();
    }

    [Singleton(typeof(NavigationService), [typeof(INavigationService)])]
    private static partial void Configure(IServiceCollection services);

    [Transient(typeof(ShellView))]
    [Transient(typeof(PrayersView))]
    [Transient(typeof(TasbihView))]
    [Transient(typeof(SettingsView))]
    private static partial void ConfigureViews(IServiceCollection services);

    [Transient(typeof(ShellViewModel))]
    [Transient(typeof(PrayersViewModel))]
    [Transient(typeof(TasbihViewModel))]
    [Transient(typeof(SettingsViewModel))]
    private static partial void ConfigureViewModels(IServiceCollection services);
}