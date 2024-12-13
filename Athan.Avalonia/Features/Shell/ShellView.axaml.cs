using System;
using Avalonia.Controls;
using Avalonia.Platform;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellView : Window
{
    public ShellView(ShellViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    protected override void OnOpened(EventArgs eventArgs)
    {
        base.OnOpened(eventArgs);
    }
}