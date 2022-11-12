namespace Athan.Services.Extensions;

public static class HttpClientExtension
{
    public static async Task<HttpResponseMessage?> TryGetAsync(this HttpClient client, string requestUrl)
    {
        try
        {
            var response = await client.GetAsync(requestUrl);
            return response.IsSuccessStatusCode ? response : null;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}