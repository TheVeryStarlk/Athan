using System.IO;
using System.Text.Json;

namespace Athan.Desktop.Features.Settings;

public sealed class SettingsService
{
    private readonly string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "athan.json");

    private Dictionary<string, string?> settings = [];

    public async Task InitializeAsync()
    {
        try
        {
            var file = await File.ReadAllTextAsync(path);
            settings = JsonSerializer.Deserialize<Dictionary<string, string?>>(file)!;
        }
        catch
        {
            // Nothing.
        }
    }

    public async Task SaveAsync()
    {
        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(settings));
    }

    public T? Get<T>(string key)
    {
        return settings.TryGetValue(key, out var value) ? JsonSerializer.Deserialize<T>(value!) : default;
    }

    public void Set<T>(string key, T? value)
    {
        settings[key] = JsonSerializer.Serialize(value);
    }
}