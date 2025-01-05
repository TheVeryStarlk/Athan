using System;
using System.Text;

namespace Athan.Avalonia.Models;

internal sealed class Prayer(string name, TimeSpan when)
{
    public string Name => name;

    public string Time => DateTime.Now.Add(when).ToString("t");

    public string Message
    {
        get
        {
            var hours = Math.Abs(when.Hours);
            var minutes = Math.Abs(when.Minutes);

            if (hours is 0 && minutes is 0)
            {
                return "Now.";
            }

            var builder = new StringBuilder(when.Hours < 0 ? "Before " : "After ");

            if (hours > 0)
            {
                builder.Append($"{hours} hours");
            }

            if (minutes > 0)
            {
                var prefix = hours > 0 ? " and " : string.Empty;
                builder.Append($"{prefix}{minutes} minutes");
            }

            builder.Append('.');

            return builder.ToString();
        }
    }
}