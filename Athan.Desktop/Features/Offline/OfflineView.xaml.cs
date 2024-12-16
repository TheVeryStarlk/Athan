using System.Windows.Controls;

namespace Athan.Desktop.Features.Offline;

public sealed partial class OfflineView : UserControl
{
    public OfflineView(OfflineViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}