using Avalonia.Themes.Fluent;

namespace Athan.Avalonia.Models;

internal sealed record Setting(Location? Location, FluentThemeMode Theme, ApplicationLanguage Language)
{
    public bool Validate()
    {
        return Location is not null
               && !string.IsNullOrWhiteSpace(Location.City)
               && !string.IsNullOrWhiteSpace(Location.Country);
    }
}