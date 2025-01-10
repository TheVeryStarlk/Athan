using System;
using System.Text;

namespace Athan.Avalonia.Features.Prayers;

internal sealed class Prayer(string name, TimeSpan when)
{
    public string Name => name;

    public TimeSpan When => when;

    public string Time => DateTime.Now.Add(when).ToString("t");

    public string Message
    {
        get
        {
            if (when.Hours is 0 && when.Minutes is 0)
            {
                return "Now.";
            }

            var builder = new StringBuilder("After ");

            if (when.Hours > 0)
            {
                builder.Append($"{when.Hours} hours");
            }

            if (when.Minutes > 0)
            {
                var prefix = when.Hours > 0 ? " and " : string.Empty;
                builder.Append($"{prefix}{when.Minutes} minutes");
            }

            builder.Append('.');

            return builder.ToString();
        }
    }
}