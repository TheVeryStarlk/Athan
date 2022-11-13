using Avalonia.Themes.Fluent;

namespace Athan.Avalonia.Services;

internal sealed class ThemeService
{
    public FluentThemeMode Theme => FluentTheme!.Mode;

    private FluentTheme? FluentTheme => (FluentTheme?) App.Current.Styles.FirstOrDefault(style => style is FluentTheme);

    public void Update(FluentThemeMode theme)
    {
        if (FluentTheme is null)
        {
            App.Current.Styles.Add(
                new FluentTheme(new Uri($"avares://Avalonia.Themes.Fluent/Accents/Base{theme}.xaml")));
        }

        FluentTheme!.Mode = theme;
    }
}