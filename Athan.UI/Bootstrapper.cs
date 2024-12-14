using Athan.UI.Features.Shell;
using Athan.UI.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ShellView>();
        services.AddSingleton<ShellViewModel>();

        services.AddSingleton<WelcomeView>();
        services.AddSingleton<WelcomeViewModel>();

        services.AddTransient<LocationService>();

        return services.BuildServiceProvider();
    }
}