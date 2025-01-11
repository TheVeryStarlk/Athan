using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace Athan.Avalonia.Features.Shell;

internal sealed class CaptionBorder : Border
{
    public CaptionType Type { get; set; }

    protected override void OnPointerEntered(PointerEventArgs eventArgs)
    {
        base.OnPointerEntered(eventArgs);
        Background = Type is CaptionType.Close ? Brushes.Red : Brushes.DimGray;
    }

    protected override void OnPointerPressed(PointerPressedEventArgs eventArgs)
    {
        base.OnPointerPressed(eventArgs);
        Opacity = 0.75D;
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs eventArgs)
    {
        base.OnPointerReleased(eventArgs);
        Opacity = 1;
    }

    protected override void OnPointerExited(PointerEventArgs eventArgs)
    {
        base.OnPointerExited(eventArgs);
        Background = Brushes.Transparent;
    }
}

internal enum CaptionType
{
    Close,
    Minimize
}