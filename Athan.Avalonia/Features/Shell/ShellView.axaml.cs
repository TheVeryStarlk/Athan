using Athan.Avalonia.Extensions;
using Avalonia.Controls;
using Microsoft.UI.Windowing;
using Microsoft.UI;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellView : Window
{
    private readonly ShellViewModel viewModel;

    public ShellView(ShellViewModel viewModel)
    {
        this.viewModel = viewModel;

        DataContext = viewModel;
        InitializeComponent();

        var handle = GetTopLevel(this)!.TryGetPlatformHandle()!.Handle;
        var window = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(handle));

        if (window.Presenter is OverlappedPresenter presenter)
        {
            presenter.IsMinimizable = false;
            presenter.IsMaximizable = false;
            presenter.IsResizable = false;
        }

        window.TitleBar.ExtendsContentIntoTitleBar = true;
        window.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
        window.TitleBar.ButtonBackgroundColor = Colors.Transparent;
        window.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        TitleArea.Height = window.TitleBar.Height;
    }

    public void UpdateState(bool show)
    {
        WindowState = show ? WindowState.Normal : WindowState.Minimized;
        ShowInTaskbar = show;
        IsVisible = show;
    }

    protected override void OnClosing(WindowClosingEventArgs eventArgs)
    {
        base.OnClosing(eventArgs);

        if (eventArgs.IsProgrammatic)
        {
            return;
        }

        eventArgs.Cancel = true;

        UpdateState(false);

        viewModel.MinimizedAsync().Await();
    }
}