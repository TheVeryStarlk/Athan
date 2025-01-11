using Avalonia;
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
        var result = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(handle));

        if (result.Presenter is OverlappedPresenter presenter)
        {
            presenter.IsMinimizable = true;
            presenter.IsMaximizable = false;
        }

        result.TitleBar.ExtendsContentIntoTitleBar = true;
        result.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

        TitleArea.Height = result.TitleBar.Height;
    }

    protected override async void OnPropertyChanged(AvaloniaPropertyChangedEventArgs eventArgs)
    {
        if (eventArgs.Property != WindowStateProperty)
        {
            return;
        }

        if (WindowState is WindowState.Minimized)
        {
            ShowInTaskbar = false;

            await viewModel.MinimizedAsync();
        }

        base.OnPropertyChanged(eventArgs);
    }
}