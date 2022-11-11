using System.Text.Json;
using Athan.Services.Extensions;
using Athan.Services.Models;

namespace Athan.Services;

public sealed class LocationService
{
    private readonly HttpClient httpClient;

    public LocationService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Location?> GetLocationAsync()
    {
        var request = await httpClient.TryGetAsync("http://ip-api.com/json/?fields=city,country");

        if (request is null)
        {
            return null;
        }

        var location = JsonSerializer.Deserialize<Location>(await request.Content.ReadAsStringAsync(),
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        return location;
    }
}