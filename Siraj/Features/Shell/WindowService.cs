using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Siraj.Features.Shell;

internal sealed class WindowService
{
    private ShellView? window;
    
    public void Open()
    {
        window ??= Bootstrapper.Services.GetRequiredService<ShellView>();
        window.Activate();
    }

    public void Exit()
    {
        Application.Current.Exit();
    }
}