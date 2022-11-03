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

    public async Task<Setting> UpdateAsync(Setting setting)
    {
        var json = JsonSerializer.Serialize(setting);
        await File.WriteAllTextAsync(path, json);

        return setting;
    }

    public async Task<Setting?> ReadAsync()
    {
        try
        {
            return JsonSerializer.Deserialize<Setting>(await File.ReadAllTextAsync(path))!;
        }
        catch (Exception)
        {
            return null;
        }
    }
}