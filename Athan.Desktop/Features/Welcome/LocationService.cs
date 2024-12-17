using System.Net.Http;
using System.Text.Json;
using Athan.Desktop.Extensions;
using FluentResults;

namespace Athan.Desktop.Features.Welcome;

public sealed class LocationService(HttpClient httpClient)
{
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<Result<Location>> GetLocationAsync()
    {
        var request = await httpClient.TryGetAsync("http://ip-api.com/json/?fields=city,country");

        if (request.IsFailed)
        {
            return Result.Fail(request.Errors);
        }

        var location = JsonSerializer.Deserialize<Location>(await request.Value.Content.ReadAsStringAsync(), options);
        return Result.Ok(location!);
    }
}

public sealed record Location(string City, string Country)
{
    public override string ToString()
    {
        return $"{City}, {Country}";
    }
}