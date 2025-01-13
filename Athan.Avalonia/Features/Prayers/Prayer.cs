using System;
using System.Text;

namespace Athan.Avalonia.Features.Prayers;

internal sealed class Prayer(string name, TimeSpan after)
{
    public string Name => name;

    public TimeSpan After => after;

    public string Time => DateTime.Now.Add(after).ToString("t");

    public string Message
    {
        get
        {
            if (after.Hours is 0 && after.Minutes is 0)
            {
                return "Now.";
            }

            var builder = new StringBuilder("After ");

            if (after.Hours > 0)
            {
                builder.Append($"{after.Hours} hours");
            }

            if (after.Minutes > 0)
            {
                var prefix = after.Hours > 0 ? " and " : string.Empty;
                builder.Append($"{prefix}{after.Minutes} minutes");
            }

            return builder.ToString();
        }
    }
}