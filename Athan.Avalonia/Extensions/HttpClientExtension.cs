using FluentResults;

namespace Athan.Avalonia.Extensions;

public static class HttpClientExtension
{
    public static async Task<Result<HttpResponseMessage>> TryGetAsync(this HttpClient client, string requestUrl)
    {
        try
        {
            var response = await client.GetAsync(requestUrl);

            return response.IsSuccessStatusCode
                ? Result.Ok(response)
                : Result.Fail("Request response does not indicate success.");
        }
        catch (HttpRequestException)
        {
            return Result.Fail("An error has occured while processing the request.");
        }
    }
}