namespace Athan.Avalonia.Models;

internal sealed record Location(string City, string Country)
{
    public override string ToString()
    {
        return $"{City}, {Country}";
    }
}