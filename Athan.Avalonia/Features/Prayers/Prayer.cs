using System;
using System.Text;

namespace Athan.Avalonia.Features.Prayers;

internal sealed record Prayer(string Emoji, string Name, TimeSpan After)
{
    public string Time => DateTime.Now.Add(After).ToString("t");

    public string Message
    {
        get
        {
            if (After.Hours is 0 && After.Minutes is 0)
            {
                return "Now.";
            }

            var builder = new StringBuilder("After ");

            if (After.Hours > 0)
            {
                builder.Append($"{After.Hours} hours");
            }

            if (After.Minutes > 0)
            {
                var prefix = After.Hours > 0 ? " and " : string.Empty;
                builder.Append($"{prefix}{After.Minutes} minutes");
            }

            return builder.ToString();
        }
    }
}