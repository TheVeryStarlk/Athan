namespace Athan.Services.Extensions;

public static class HttpClientExtension
{
    public static async Task<HttpResponseMessage?> TryGetAsync(this HttpClient client, string url)
    {
        try
        {
            var response = await client.GetAsync(url);
            return response.IsSuccessStatusCode ? response : null;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}