using System.Text.Json;
using Athan.Services.Extensions;
using Athan.Services.Models;
using FluentResults;

namespace Athan.Services;

public sealed class LocationService
{
    private readonly HttpClient httpClient;

    public LocationService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<Location>> GetLocationAsync()
    {
        var request = await httpClient.TryGetAsync("http://ip-api.com/json/?fields=city,country");

        if (request.IsFailed)
        {
            return Result.Fail(request.Errors);
        }

        var location = JsonSerializer.Deserialize<Location>(await request.Value.Content.ReadAsStringAsync(),
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        return Result.Ok(location!);
    }
}