using Athan.Avalonia.Languages;
using FluentResults;

namespace Athan.Avalonia.Extensions;

public static class HttpClientExtension
{
    public static async Task<Result<HttpResponseMessage>> TryGetAsync(this HttpClient client, string requestUrl)
    {
        try
        {
            var response = await client.GetAsync(requestUrl);

            return response.IsSuccessStatusCode ? Result.Ok(response) : Result.Fail(Language.RequestErrorOccured);
        }
        catch (HttpRequestException)
        {
            return Result.Fail(Language.RequestErrorOccured);
        }
    }
}