using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using Athan.Desktop.Extensions;
using FluentResults;

namespace Athan.Desktop.Features.Prayers;

public sealed class PrayerService(HttpClient httpClient)
{
    public async Task<Result<Prayer[]>> GetPrayersAsync(string city, string country)
    {
        var request = await httpClient.TryGetAsync($"http://api.aladhan.com/v1/timingsByCity?city={city}&country={country}");

        if (request.IsFailed)
        {
            return Result.Fail(request.Errors);
        }

        var json = JsonNode.Parse(await request.Value.Content.ReadAsStringAsync());
        var timings = json?["data"]?["timings"].Deserialize<Dictionary<string, string>>()!;

        var prayers = new Prayer[5];
        var index = 0;

        foreach (var timing in timings)
        {
            var name = timing.Key switch
            {
                "Fajr" or "Dhuhr" or "Asr" or "Maghrib" or "Isha" => timing.Key,
                _ => null
            };

            if (name is null)
            {
                continue;
            }

            var time = DateTime.Parse(timing.Value);

            prayers[index++] = new Prayer(
                name,
                time.ToShortTimeString(),
                time);
        }

        return Result.Ok(prayers);
    }
}