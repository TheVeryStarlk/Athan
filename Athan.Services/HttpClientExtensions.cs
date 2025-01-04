using LightResults;

namespace Athan.Services;

internal static class HttpClientExtension
{
    public static async Task<Result<HttpResponseMessage>> TryGetAsync(this HttpClient client, string link)
    {
        try
        {
            var response = await client.GetAsync(link);

            return response.IsSuccessStatusCode
                ? Result.Success(response)
                : Result.Failure<HttpResponseMessage>("The request was not successful.");
        }
        catch (HttpRequestException)
        {
            return Result.Failure<HttpResponseMessage>("An error has occured while requesting information.");
        }
    }
}