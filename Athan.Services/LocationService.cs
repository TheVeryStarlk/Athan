using System.Text.Json;
using Athan.Services.Models;

namespace Athan.Services;

public sealed class LocationService
{
    private sealed record Root(string Status, string Country, string CountryCode, string Region, string RegionName,
        string City, string Zip, float Lat, float Lon, string Timezone, string Isp, string Org, string As, string Query);

    private readonly HttpClient httpClient;

    public LocationService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Location?> GetLocationAsync()
    {
        var locationRequest = await httpClient.GetAsync("http://ip-api.com/json");
        var root = JsonSerializer.Deserialize<Root>(await locationRequest.Content.ReadAsStringAsync(),
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        return root is null ? null : new Location(root.City, root.Country);
    }
}