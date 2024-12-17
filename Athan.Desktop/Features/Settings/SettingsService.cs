using System.IO;
using System.Text.Json;
using Athan.Desktop.Features.Welcome;

namespace Athan.Desktop.Features.Settings;

public sealed class SettingsService
{
    private readonly string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "athan.json");

    public async Task<SettingsModel?> LoadAsync()
    {
        try
        {
            var file = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<SettingsModel>(file);
        }
        catch
        {
            return null;
        }
    }

    public async Task SaveAsync(Location location)
    {
        var settings = await LoadAsync() ?? new SettingsModel(location);

        settings.Location = location;

        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(settings));
    }
}

public sealed class SettingsModel(Location location)
{
    public Location? Location { get; set; } = location;
}