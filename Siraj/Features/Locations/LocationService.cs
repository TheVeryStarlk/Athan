using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Siraj.Features.Locations;

internal sealed class LocationService(IHttpClientFactory clientFactory)
{
    private const string Url = "https://nominatim.openstreetmap.org/";

    public async Task<Location[]> SearchAsync(string query)
    {
        var client = clientFactory.CreateClient();

        client.DefaultRequestHeaders.Add("User-Agent", nameof(Siraj));
        client.DefaultRequestHeaders.Add("Accept-Language", "en-US");

        var result = await client.GetFromJsonAsync(
            $"{Url}search?q={Uri.EscapeDataString(query)}&format=jsonv2",
            SirajSerializerContext.Default.LocationArray);

        ArgumentNullException.ThrowIfNull(result);

        return result;
    }

    public async Task<Location> ReverseAsync(double latitude, double longitude)
    {
        var client = clientFactory.CreateClient();

        client.DefaultRequestHeaders.Add("User-Agent", nameof(Siraj));
        client.DefaultRequestHeaders.Add("Accept-Language", "en-US");

        var result = await client.GetFromJsonAsync(
            $"{Url}reverse?lat={latitude}&lon={longitude}&zoom=18&format=jsonv2",
            SirajSerializerContext.Default.Location);

        ArgumentNullException.ThrowIfNull(result);

        return result;
    }
}