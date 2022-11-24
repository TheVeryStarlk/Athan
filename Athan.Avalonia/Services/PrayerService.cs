using Athan.Avalonia.Models;
using Athan.Avalonia.Extensions;
using Athan.Avalonia.Languages;
using FluentResults;
using Newtonsoft.Json.Linq;

namespace Athan.Avalonia.Services;

public sealed class PrayerService
{
    private readonly HttpClient httpClient;

    public PrayerService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Result<Prayer[]?>> GetTimingsAsync(string city, string country)
    {
        var request =
            await httpClient.TryGetAsync($"http://api.aladhan.com/v1/timingsByCity?city={city}&country={country}");

        if (request.IsFailed)
        {
            return Result.Fail(request.Errors);
        }

        var json = JObject.Parse(await request.Value.Content.ReadAsStringAsync());

        var timings = json["data"]?["timings"]?
            .ToObject<Dictionary<string, string>>()?
            .Take(7);

        var prayers = timings?
            .Select(prayer =>
            {
                var date = DateTime.Parse(prayer.Value);

                var name = prayer.Key switch
                {
                    "Fajr" => Language.Fajr,
                    "Dhuhr" => Language.Dhuhr,
                    "Asr" => Language.Asr,
                    "Maghrib" => Language.Maghrib,
                    "Isha" => Language.Isha,
                    _ => string.Empty
                };

                return new Prayer(name, date.ToShortTimeString(), date);
            })
            .Where(prayer => !string.IsNullOrWhiteSpace(prayer.Name))
            .ToArray();

        return Result.Ok(prayers);
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
            return index >= prayers.Length ? prayers[0] : prayers[index];
        }

        return closestPrayer;
    }
}