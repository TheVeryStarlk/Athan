using System;
using Avalonia.Controls;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Serilog;

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

        TitleArea.Height = window.TitleBar.Height;
    }

    protected override async void OnClosing(WindowClosingEventArgs eventArgs)
    {
        try
        {
            base.OnClosing(eventArgs);

            if (eventArgs.IsProgrammatic)
            {
                return;
            }

            eventArgs.Cancel = true;

            ShowInTaskbar = false;
            IsVisible = false;
            WindowState = WindowState.Minimized;

            await viewModel.MinimizedAsync();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "A fatal exception occured.");
        }
    }
}