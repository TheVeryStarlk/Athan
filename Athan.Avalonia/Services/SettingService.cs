using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class SettingService
{
    private readonly string path =
        Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(Athan));

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
            return JsonSerializer.Deserialize<Setting>(await File.ReadAllTextAsync(path));
        }
        catch (Exception)
        {
            return null;
        }
    }
}