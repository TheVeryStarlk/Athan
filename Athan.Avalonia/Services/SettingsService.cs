using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class SettingsService
{
    private readonly string path =
        Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), nameof(Athan));

    public async Task<Settings> UpdateAsync(Settings settings)
    {
        var json = JsonSerializer.Serialize(settings);
        await File.WriteAllTextAsync(path, json);
        
        return settings;
    }

    public async Task<Settings> ReadAsync()
    {
        try
        {
            return JsonSerializer.Deserialize<Settings>(await File.ReadAllTextAsync(path))!;
        }
        catch (FileNotFoundException)
        {
            var settings = new Settings(null);
            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(settings));
            
            return settings;
        }
    }
}