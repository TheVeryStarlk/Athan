using System;
using System.Threading.Tasks;
using Athan.UI.Features.Shell;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Empty;

internal sealed class DialogService
{
    private XamlRoot? root;
    
    public async Task ShowMessageAsync(string title, string message)
    {
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