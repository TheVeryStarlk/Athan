using System.Text.Json;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class SettingService
{
    private readonly string path = Path.Join(App.Directory, "Settings");

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
            await using var file = File.Open(path, FileMode.OpenOrCreate);
            using var stream = new StreamReader(file);

            return JsonSerializer.Deserialize<Setting>(await stream.ReadToEndAsync());
        }
        catch (JsonException)
        {
            return null;
        }
    }
}