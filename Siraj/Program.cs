using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using WinRT;

namespace Siraj;

internal static partial class Program
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
        var instance = AppInstance.FindOrRegisterForKey(nameof(Siraj));
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
    
    [LibraryImport("kernel32.dll", StringMarshalling = StringMarshalling.Utf16)]
    private static partial nint CreateEvent(nint lpEventAttributes, [MarshalAs(UnmanagedType.Bool)] bool bManualReset, [MarshalAs(UnmanagedType.Bool)] bool bInitialState, string? lpName);

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetEvent(nint hEvent);

    [LibraryImport("ole32.dll")]
    private static partial uint CoWaitForMultipleObjects(uint dwFlags, uint dwMilliseconds, ulong nHandles, nint[] pHandles, out uint dwIndex);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetForegroundWindow(nint hWnd);
}