using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace Athan.UI;

internal static class WindowExtensions
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr handle, int command);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr handle);

    public static void Show(this Window window)
    {
        var handle = WindowNative.GetWindowHandle(window);

        ShowWindow(handle, 0x00000009);
        SetForegroundWindow(handle);
    }
}