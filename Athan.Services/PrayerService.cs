using Athan.Services.Extensions;
using Athan.Services.Models;
using Newtonsoft.Json.Linq;

namespace Athan.Services;

public sealed class PrayerService
{
    private readonly HttpClient httpClient;

    public PrayerService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Prayer[]?> GetTimingsAsync(string city, string country)
    {
        var request =
            await httpClient.TryGetAsync($"http://api.aladhan.com/v1/timingsByCity?city={city}&country={country}");

        if (request is null)
        {
            return null;
        }

        var json = JObject.Parse(await request.Content.ReadAsStringAsync());

        var timings = json["data"]?["timings"]?
            .ToObject<Dictionary<string, string>>()?
            .Take(7);

        var prayers = timings?
            .Select(time =>
            {
                var date = DateTime.Parse(time.Value);
                return new Prayer(time.Key, date.ToShortTimeString(), date);
            })
            .Where(time => !time.Name.Contains("Sun"))
            .ToArray();

        return prayers;
    }

    public Prayer GetClosest(Prayer[] prayers)
    {
        var now = DateTime.Now;

        var closestPrayer = prayers
            .OrderBy(timing => Math.Abs((timing.DateTime - now).Ticks))
            .First();

        if (now > closestPrayer.DateTime)
        {
            var index = prayers.TakeWhile(prayer => prayer != closestPrayer).Count() + 1;
            return index >= prayers.Length ? prayers.First() : prayers[index];
        }

        return closestPrayer;
    }
}