using System;
using Athan.UI.Features.Shell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Athan.UI;

public sealed partial class App : Application
{
    private readonly IServiceProvider provider = Bootstrapper.Create();

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs eventArgs)
    {
        var shell = provider.GetRequiredService<ShellView>();
        shell.Activate();
    }
}