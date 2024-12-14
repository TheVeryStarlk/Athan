using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellView : Window
{
    public ShellViewModel ViewModel { get; }

    public ShellView(ShellViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SystemBackdrop = new MicaBackdrop();

        WeakReferenceMessenger.Default.Register<ReadyMessage>(
            this,
            (_, _) => SplashView.Opacity = 0);

        Activated += async (_, _) =>
        {
            await Task.Delay(TimeSpan.FromSeconds(2.5));
            WeakReferenceMessenger.Default.Send<ReadyMessage>();
        };
    }
}