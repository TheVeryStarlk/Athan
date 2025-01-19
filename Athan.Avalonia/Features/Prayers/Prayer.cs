using System;

namespace Athan.Avalonia.Features.Prayers;

internal sealed record Prayer(string Emoji, string Name, TimeSpan After)
{
    public string Time => DateTime.Now.Add(After).ToString("t");

    public string Message => (After.Hours, After.Minutes) switch
    {
        (Hours: 0, Minutes: 0) => "Now",
        (Hours: > 0, Minutes: 0) => $"After {After.Hours} hours",
        (Hours: 0, Minutes: > 0) => $"After {After.Minutes} minutes",
        (Hours: > 0, Minutes: > 0) => $"After {After.Hours} hours and {After.Minutes} minutes",
        _ => string.Empty
    };
}