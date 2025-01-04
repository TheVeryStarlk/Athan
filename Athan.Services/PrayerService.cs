using System.Collections.Frozen;
using System.Text.Json.Nodes;
using LightResults;

namespace Athan.Services;

public sealed class PrayerService(HttpClient httpClient)
{
    public async Task<Result<FrozenDictionary<string, TimeSpan>>> GetAsync(string country, string city)
    {
        var request = await httpClient.TryGetAsync($"http://api.aladhan.com/v1/timingsByCity?country={country}&city={city}");

        if (!request.IsSuccess(out var response))
        {
            return Result.Failure<FrozenDictionary<string, TimeSpan>>(request.Errors.First().Message);
        }

        await using var stream = await response.Content.ReadAsStreamAsync();

        var node = await JsonNode.ParseAsync(stream);

        var timings = node?["data"]?["timings"]!.AsObject() ?? [];

        if (timings.Count is 0)
        {
            return Result.Failure<FrozenDictionary<string, TimeSpan>>("Timings not found.");
        }

        var dictionary = new Dictionary<string, TimeSpan>();

        foreach (var pair in timings)
        {
            if (!TimeSpan.TryParse(pair.Value?.ToString(), out var result))
            {
                return Result.Failure<FrozenDictionary<string, TimeSpan>>("Could not parse timings.");
            }

            dictionary[pair.Key] = result;
        }

        return Result.Success(dictionary.ToFrozenDictionary());
    }
}