using System;
using Athan.UI.Features.Shell;
using Athan.UI.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var services = new ServiceCollection();

        services.AddTransient<ShellView>();
        services.AddTransient<ShellViewModel>();

        services.AddTransient<WelcomeView>();
        services.AddTransient<WelcomeViewModel>();

        services.AddTransient<LocationService>();

        return services.BuildServiceProvider();
    }
}