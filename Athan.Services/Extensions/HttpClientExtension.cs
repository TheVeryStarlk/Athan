namespace Athan.Services.Extensions;

public static class HttpClientExtension
{
    public static async Task<HttpResponseMessage?> TryGetAsync(this HttpClient client, string url)
    {
        try
        {
            return await client.GetAsync(url);
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}