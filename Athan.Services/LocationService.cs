using System.Net.Http.Json;
using LightResults;

namespace Athan.Services;

public sealed class LocationService(HttpClient httpClient)
{
    public async Task<Result<Location>> GetAsync()
    {
        var request = await httpClient.TryGetAsync("http://ip-api.com/json/?fields=country,city");

        if (!request.IsSuccess(out var response))
        {
            return Result.Failure<Location>(request.Errors.First().Message);
        }

        var result = await response.Content.ReadFromJsonAsync<Location>();

        return Result.Success(result!);
    }
}