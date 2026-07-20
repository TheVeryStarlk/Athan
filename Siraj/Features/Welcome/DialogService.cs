using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Siraj.Features.Shell;
using Serilog;

namespace Siraj.Features.Welcome;

internal sealed class DialogService
{
    private XamlRoot? root;

    public async Task ShowMessageAsync(string title, string message)
    {
        Log.Debug("Showing message dialog {DialogTitle}", title);

        root ??= Bootstrapper.Services.GetRequiredService<ShellView>().Content.XamlRoot;

        var dialog = new ContentDialog
        {
            XamlRoot = root,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            Title = title,
            PrimaryButtonText = "Close",
            Content = message
        };

        await dialog.ShowAsync();
    }
}