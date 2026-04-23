using Athan.UI.Features.Empty;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Shell;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static partial class Bootstrapper
{
    public static ServiceProvider Services { get; } = Build();

    private static ServiceProvider Build()
    {
        var services = new ServiceCollection();

        Configure(services);
        ConfigureViews(services);
        ConfigureViewModels(services);

        return services.BuildServiceProvider();
    }

    [Singleton(typeof(NavigationService), typeof(INavigationService))]
    [Transient(typeof(DialogService))]
    private static partial void Configure(IServiceCollection services);

    [Singleton(typeof(ShellView))]
    private static partial void ConfigureViews(IServiceCollection services);

    [Transient(typeof(ShellViewModel))]
    [Transient(typeof(PrayersViewModel))]
    [Transient(typeof(TasbihViewModel))]
    [Transient(typeof(SettingsViewModel))]
    [Transient(typeof(EmptyViewModel))]
    private static partial void ConfigureViewModels(IServiceCollection services);
}