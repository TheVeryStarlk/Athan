using Athan.Desktop.Features;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Desktop;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var collection = new ServiceCollection();

        collection.AddTransient<ShellView>();
        collection.AddTransient<ShellViewModel>();

        collection.AddSingleton<NavigationService>();

        collection.AddSingleton<WelcomeView>();
        collection.AddSingleton<WelcomeViewModel>();

        collection.AddSingleton<SettingsView>();
        collection.AddSingleton<SettingsViewModel>();

        return collection.BuildServiceProvider();
    }
}