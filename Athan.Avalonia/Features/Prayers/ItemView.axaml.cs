using Avalonia.Controls;
using Avalonia.Input;

namespace Athan.Avalonia.Features.Prayers;

internal sealed partial class ItemView : UserControl
{
    public ItemView()
    {
        InitializeComponent();
    }

    private void ItemOnPointerEntered(object? sender, PointerEventArgs eventArgs)
    {
        TimeText.Opacity = 0;
        MessageText.Opacity = 1;
    }

    private void ItemOnPointerExited(object? sender, PointerEventArgs eventArgs)
    {
        TimeText.Opacity = 1;
        MessageText.Opacity = 0;
    }
}