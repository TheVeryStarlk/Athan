using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Athan.Desktop.Extensions;

internal static class SnackbarServiceExtensions
{
    public static void Show(this SnackbarService service, string title, string description, SymbolRegular symbolRegular)
    {
        service.Show(
            title,
            description,
            ControlAppearance.Transparent,
            new SymbolIcon(symbolRegular),
            TimeSpan.FromSeconds(5));
    }
}