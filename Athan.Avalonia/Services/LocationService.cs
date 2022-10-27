using System.Net.Http;
using System.Threading.Tasks;
using Athan.Avalonia.Models;
using Newtonsoft.Json.Linq;

namespace Athan.Avalonia.Services;

internal sealed class LocationService
{
    private readonly HttpClient httpClient;

    public LocationService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Location> GetLocationAsync()
    {
        var locationRequest = await httpClient.GetAsync("http://ip-api.com/json");
        var location = JObject.Parse(await locationRequest.Content.ReadAsStringAsync());

        return new Location(location["city"]?.ToObject<string>()!, location["country"]?.ToObject<string>()!);
    }
}