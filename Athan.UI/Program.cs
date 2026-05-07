using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using WinRT;

namespace Athan.UI;

internal static class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        ComWrappersSupport.InitializeComWrappers();

        if (IsRedirect())
        {
            return 0;
        }

        Application.Start(_ =>
        {
            var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());

            SynchronizationContext.SetSynchronizationContext(context);

            new App();
        });

        return 0;
    }

    private static bool IsRedirect()
    {
        var instance = AppInstance.FindOrRegisterForKey(nameof(Athan));
        var redirect = !instance.IsCurrent;
        
        if (redirect)
        {
            RedirectActivation(AppInstance.GetCurrent().GetActivatedEventArgs(), instance);
        }

        return redirect;
    }
    
    private static void RedirectActivation(AppActivationArguments args, AppInstance instance)
    {
        var handle = CreateEvent(nint.Zero, true, false, null);

        Task.Run(() =>
        {
            instance.RedirectActivationToAsync(args).AsTask().Wait();
            SetEvent(handle);
        });

        _ = CoWaitForMultipleObjects(0, 0xFFFFFFFF, 1, [handle], out _);

        SetForegroundWindow(Process.GetProcessById((int) instance.ProcessId).MainWindowHandle);
    }
    
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern nint CreateEvent(nint lpEventAttributes, bool bManualReset, bool bInitialState, string? lpName);

    [DllImport("kernel32.dll")]
    private static extern bool SetEvent(nint hEvent);

    [DllImport("ole32.dll")]
    private static extern uint CoWaitForMultipleObjects(uint dwFlags, uint dwMilliseconds, ulong nHandles, nint[] pHandles, out uint dwIndex);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(nint hWnd);
}