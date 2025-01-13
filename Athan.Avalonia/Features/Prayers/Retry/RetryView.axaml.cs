using Avalonia.Controls;

namespace Athan.Avalonia.Features.Prayers.Retry;

internal sealed partial class RetryView : UserControl
{
    public RetryView(RetryViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}