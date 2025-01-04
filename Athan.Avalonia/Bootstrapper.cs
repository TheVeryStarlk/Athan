using System;
using Athan.Avalonia.ViewModels;
using Athan.Avalonia.Views;
using CommunityToolkit.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

internal static partial class Bootstrapper
{
    public static IServiceProvider Build()
    {
        var services = new ServiceCollection();

        ConfigureServices(services);

        return services.BuildServiceProvider();
    }

    [Transient(typeof(ShellViewModel))]
    [Transient(typeof(ShellView))]
    private static partial void ConfigureServices(IServiceCollection services);
}