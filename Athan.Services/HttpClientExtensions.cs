using LightResults;

namespace Athan.Services;

internal static class HttpClientExtension
{
    public static async Task<Result<HttpResponseMessage>> TryGetAsync(this HttpClient client, string link)
    {
        try
        {
            var response = await client.GetAsync(link);

            var message = response.EnsureSuccessStatusCode();

            return Result.Success(message);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<HttpResponseMessage>("The request was not successful.");
        }
    }
}