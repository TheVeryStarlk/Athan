using System.Linq;
using Avalonia.Themes.Fluent;

namespace Athan.Avalonia.Services;

internal sealed class ThemeService
{
    public FluentThemeMode Theme => ((FluentTheme) App.Current.Styles.First(style => style is FluentTheme)).Mode;

    public void Update(FluentThemeMode mode)
    {
        var theme = (FluentTheme) App.Current.Styles.First(style => style is FluentTheme);
        theme.Mode = mode;
    }
}