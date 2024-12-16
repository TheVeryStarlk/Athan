using System.Net.Http;
using FluentResults;

namespace Athan.Desktop.Features;

public static class HttpClientExtension
{
    public static async Task<Result<HttpResponseMessage>> TryGetAsync(this HttpClient client, string requestUrl)
    {
        const string message = "An error has occured while requesting information.";

        try
        {
            var response = await client.GetAsync(requestUrl);
            return response.IsSuccessStatusCode ? Result.Ok(response) : Result.Fail(message);
        }
        catch (HttpRequestException)
        {
            return Result.Fail(message);
        }
    }
}