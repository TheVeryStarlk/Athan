using System.IO;
using System.Text.Json;
using Serilog;

namespace Athan.Desktop.Features.Settings;

public sealed class SettingsService(ILogger logger)
{
    private readonly string path = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Athan",
        "settings.json");

    private Dictionary<string, string?> settings = [];

    public async Task InitializeAsync()
    {
        logger.Information("Initializing settings service");

        try
        {
            var file = await File.ReadAllTextAsync(path);
            settings = JsonSerializer.Deserialize<Dictionary<string, string?>>(file)!;
        }
        catch (Exception exception)
        {
            logger.Error("Could not load settings: \"{Message}\"", exception.Message);

        }
    }

    public async Task SaveAsync()
    {
        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(settings));
        logger.Information("Saved settings");
    }

    public T? Get<T>(string key)
    {
        logger.Debug("Getting key: \"{Key}\"", key);
        return settings.TryGetValue(key, out var value) ? JsonSerializer.Deserialize<T>(value!) : default;
    }

    public void Set<T>(string key, T? value)
    {
        logger.Debug("Setting key: \"{Key}\", with value: \"{Value}\"", key, value);
        settings[key] = JsonSerializer.Serialize(value);
    }
}