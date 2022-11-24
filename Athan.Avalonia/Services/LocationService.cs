using System.Text.Json;
using Athan.Avalonia.Models;
using Athan.Avalonia.Extensions;
using FluentResults;

namespace Athan.Avalonia.Services;

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