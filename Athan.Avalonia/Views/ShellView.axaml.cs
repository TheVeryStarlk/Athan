using Athan.Avalonia.Messages;
using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia.Views;

internal sealed partial class ShellView : Window
{
    private WindowState oldState;

    public ShellView()
    {
        DataContext = App.Current.Services.GetRequiredService<ShellViewModel>();
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register<OpenTrayIconMessage>(this, OpenTrayIconMessageHandler);
        Margin = OffScreenMargin;
    }

    private void OpenTrayIconMessageHandler(object recipient, OpenTrayIconMessage message)
    {
        WindowState = WindowState is WindowState.Minimized
            ? oldState
            : WindowState;
    }

    protected override void HandleWindowStateChanged(WindowState state)
    {
        if (state is not WindowState.Minimized)
        {
            oldState = state;
        }

        base.HandleWindowStateChanged(state);
    }
}