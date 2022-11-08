using System.Linq;
using Avalonia.Themes.Fluent;

namespace Athan.Avalonia.Services;

internal sealed class ThemeService
{
    public FluentThemeMode Theme => style.Mode;

    private readonly FluentTheme style = (FluentTheme) App.Current.Styles.First(style => style is FluentTheme);

    public void Update(FluentThemeMode theme)
    {
        style.Mode = theme;
    }
}