using CommunityToolkit.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static partial class Bootstrapper
{
    public static ServiceProvider Build()
    {
        var services = new ServiceCollection();

        ConfigureViews(services);
        ConfigureViewModels(services);

        return services.BuildServiceProvider();
    }

    [Transient(typeof(ShellView))]
    [Transient(typeof(PrayersView))]
    [Transient(typeof(TasbihCountingView))]
    [Transient(typeof(SettingsView))]
    private static partial void ConfigureViews(IServiceCollection services);

    [Transient(typeof(ShellViewModel))]
    [Transient(typeof(PrayersViewModel))]
    [Transient(typeof(TasbihCountingViewModel))]
    [Transient(typeof(SettingsViewModel))]
    private static partial void ConfigureViewModels(IServiceCollection services);
}