using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Athan.UI.Features.Locations;

internal sealed class LocationService(IHttpClientFactory clientFactory)
{
    private const string url = "https://nominatim.openstreetmap.org/";

    public async Task<Location[]> SearchAsync(string query)
    {
        var client = clientFactory.CreateClient();

        client.DefaultRequestHeaders.Add("User-Agent", "Athan");
        client.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentUICulture.Name);

        // https://nominatim.openstreetmap.org/search?q=Riyadh&format=jsonv2

        var result = await client.GetFromJsonAsync(
            $"{url}search?q={Uri.EscapeDataString(query)}&format=jsonv2",
            AthanSerializerContext.Default.LocationArray);

        ArgumentNullException.ThrowIfNull(result);

        return result;
    }

    public async Task<Location> ReverseAsync(double latitude, double longitude)
    {
        var client = clientFactory.CreateClient();

        client.DefaultRequestHeaders.Add("User-Agent", "Athan");
        client.DefaultRequestHeaders.Add("Accept-Language", CultureInfo.CurrentUICulture.Name);

        var result = await client.GetFromJsonAsync(
            $"{url}reverse?lat={latitude}&lon={longitude}&zoom=5&format=jsonv2",
            AthanSerializerContext.Default.Location);

        ArgumentNullException.ThrowIfNull(result);

        return result;
    }
}