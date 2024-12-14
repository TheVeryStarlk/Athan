using Athan.UI.Features.Shell;
using Athan.UI.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var services = new ServiceCollection();

        services.AddView<ShellView, ShellViewModel>();
        services.AddView<WelcomeView, WelcomeViewModel>();

        services.AddTransient<LocationService>();

        return services.BuildServiceProvider();
    }
}