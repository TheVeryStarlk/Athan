using System.Net.Http;
using Athan.Desktop.Features.Offline;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;

namespace Athan.Desktop;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<SnackbarService>();
        collection.AddSingleton<HttpClient>();

        collection.AddTransient<ShellView>();
        collection.AddTransient<ShellViewModel>();
        collection.AddSingleton<NavigationService>();

        collection.AddSingleton<WelcomeView>();
        collection.AddSingleton<WelcomeViewModel>();
        collection.AddTransient<LocationService>();

        collection.AddSingleton<SettingsView>();
        collection.AddSingleton<SettingsViewModel>();

        collection.AddSingleton<OfflineView>();
        collection.AddSingleton<OfflineViewModel>();

        return collection.BuildServiceProvider();
    }
}