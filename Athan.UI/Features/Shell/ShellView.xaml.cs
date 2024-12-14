using Athan.UI.Features.Welcome;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellView : Page
{
    public ShellViewModel ViewModel { get; }

    public ShellView(ShellViewModel viewModel, ViewConverter viewConverter)
    {
        ViewModel = viewModel;
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<ReadyMessage>(
            this,
            (_, _) =>
            {
                SplashView.Opacity = 0;
                Shell.Opacity = 1;

                Shell.Content = viewConverter.Convert(ViewModel.Current!);
            });

        Loaded += async (_, _) =>
        {
            await Task.Delay(TimeSpan.FromSeconds(2.5));
            WeakReferenceMessenger.Default.Send<ReadyMessage>();
        };
    }
}