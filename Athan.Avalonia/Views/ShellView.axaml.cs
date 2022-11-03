using Athan.Avalonia.Messages;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.Views;

internal sealed partial class ShellView : Window
{
    private WindowState oldState;

    public ShellView()
    {
        DataContext = ViewModelLocator.ShellViewModel;
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register<TrayIconOpenedMessage>(this, TrayIconOpenedMessageHandler);
        Margin = OffScreenMargin;
    }

    private void TrayIconOpenedMessageHandler(object recipient, TrayIconOpenedMessage message)
    {
        WindowState = WindowState is WindowState.Minimized
            ? oldState
            : WindowState;

        Activate();
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