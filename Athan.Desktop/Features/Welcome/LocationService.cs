using System.Net.Http;
using System.Text.Json;
using Athan.Desktop.Extensions;
using FluentResults;
using Serilog;

namespace Athan.Desktop.Features.Welcome;

public sealed class LocationService(ILogger logger, HttpClient httpClient)
{
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<Result<Location>> GetLocationAsync()
    {
        logger.Information("Getting location...");

        var request = await httpClient.TryGetAsync("http://ip-api.com/json/?fields=city,country");

        if (request.IsFailed)
        {
            logger.Warning("Failed to get location");
            return Result.Fail(request.Errors);
        }

        logger.Information("Successfully got location");

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