using System.Linq;
using Avalonia.Themes.Fluent;

namespace Athan.Avalonia.Services;

internal sealed class ThemeService
{
    public FluentThemeMode Theme => fluentTheme.Mode;

    private readonly FluentTheme fluentTheme = (FluentTheme) App.Current.Styles.First(style => style is FluentTheme);

    public void Update(FluentThemeMode theme)
    {
        fluentTheme.Mode = theme;
    }
}