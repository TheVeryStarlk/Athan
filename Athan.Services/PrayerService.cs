using System.Collections.Frozen;
using System.Text.Json.Nodes;
using LightResults;

namespace Athan.Services;

public sealed class PrayerService(HttpClient httpClient)
{
    public async Task<Result<FrozenDictionary<string, DateTime>>> GetAsync(string country, string city)
    {
        country = country.Replace(" ", string.Empty);

        var request = await httpClient.TryGetAsync($"https://api.aladhan.com/v1/timingsByCity?country={country}&city={city}");

        if (!request.IsSuccess(out var response))
        {
            return Result.Failure<FrozenDictionary<string, DateTime>>(request.Errors.First().Message);
        }

        await using var stream = await response.Content.ReadAsStreamAsync();

        var node = await JsonNode.ParseAsync(stream);

        var timings = node?["data"]?["timings"]!.AsObject() ?? [];

        if (timings.Count is 0)
        {
            return Result.Failure<FrozenDictionary<string, DateTime>>("Timings not found.");
        }

        var dictionary = new Dictionary<string, DateTime>();

        foreach (var timing in timings)
        {
            if (!DateTime.TryParse(timing.Value?.ToString(), out var result))
            {
                return Result.Failure<FrozenDictionary<string, DateTime>>("Could not parse timings.");
            }

            dictionary[timing.Key] = result;
        }

        return Result.Success(dictionary.ToFrozenDictionary());
    }
}