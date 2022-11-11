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

    public Setting Update(Setting setting)
    {
        loadedSetting = setting;
        return setting;
    }

    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(loadedSetting);
        await File.WriteAllTextAsync(path, json);
    }

    public async Task<Setting?> ReadAsync()
    {
        try
        {
            loadedSetting = JsonSerializer.Deserialize<Setting>(await File.ReadAllTextAsync(path));
            return loadedSetting;
        }
        catch (Exception)
        {
            return null;
        }
    }
}