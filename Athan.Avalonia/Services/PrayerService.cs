using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Athan.Avalonia.Models;
using Newtonsoft.Json.Linq;

namespace Athan.Avalonia.Services;

internal sealed class PrayerService
{
    private readonly HttpClient httpClient;

    public PrayerService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Prayer> GetTimingsAsync(string city, string country)
    {
        var request =
            await httpClient.GetAsync($"http://api.aladhan.com/v1/timingsByCity?city={city}&country={country}");

        var json = JObject.Parse(await request.Content.ReadAsStringAsync());

        var timings = json["data"]?["timings"]?
            .ToObject<Dictionary<string, string>>()?
            .Take(7).ToDictionary(key => key.Key, value => value.Value);

        return new Prayer(timings);
    }
}