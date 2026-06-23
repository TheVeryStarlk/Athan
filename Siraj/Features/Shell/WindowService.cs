using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using WinUIEx;

namespace Siraj.Features.Shell;

internal sealed class WindowService
{
    private ShellView? window;
    
    public void Open()
    {
        window ??= Bootstrapper.Services.GetRequiredService<ShellView>();

        window.WindowState = WindowState.Normal;
        window.BringToFront();
        
        window.AppWindow.Show();
    }

    public void Exit()
    {
        Application.Current.Exit();
    }
}