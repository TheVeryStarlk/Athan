namespace Athan.Services.Models;

public sealed record Location(string City, string Country)
{
    public override string ToString()
    {
        return $"{City}, {Country}";
    }
}