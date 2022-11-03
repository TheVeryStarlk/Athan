using System.Linq;
using Avalonia.Themes.Fluent;

namespace Athan.Avalonia.Services;

internal sealed class ThemeService
{
    public void Toggle()
    {
        var theme = (FluentTheme) App.Current.Styles.First(style => style is FluentTheme);
        theme.Mode = theme.Mode is FluentThemeMode.Dark ? FluentThemeMode.Light : FluentThemeMode.Dark;
    }
}