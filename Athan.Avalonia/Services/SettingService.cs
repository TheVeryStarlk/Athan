using System.Text.Json;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class SettingService
{
    private readonly string path = Path.Join(App.Directory, "Settings.json");

    private Setting? loadedSetting;

    public void Update(Setting setting)
    {
        loadedSetting = setting;
    }

    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(loadedSetting ?? await ReadAsync());
        await File.WriteAllTextAsync(path, json);
    }

    public async Task<Setting?> ReadAsync()
    {
        if (loadedSetting?.Validate() is true)
        {
            return loadedSetting;
        }

        try
        {
            if (!File.Exists(path))
            {
                await using var stream = File.Create(path);
            }

            return JsonSerializer.Deserialize<Setting>(await File.ReadAllTextAsync(path));
        }
        catch (JsonException)
        {
            return null;
        }
    }
}