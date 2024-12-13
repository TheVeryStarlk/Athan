using System;
using Athan.UI.Features.Shell;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var services = new ServiceCollection();

        services.AddTransient<ShellView>();
        services.AddTransient<ShellViewModel>();

        return services.BuildServiceProvider();
    }
}