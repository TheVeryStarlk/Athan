using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class SettingsService
{
    private readonly string path = 
        Path.Combine(Environment.SpecialFolder.CommonApplicationData.ToString(), nameof(Athan));

    public async Task UpdateAsync(Settings settings)
    {
        var json = JsonSerializer.Serialize(settings);
        await File.WriteAllTextAsync(path, json);
    }
    
    public async Task<Settings> ReadAsync()
    {
        try
        {
            return JsonSerializer.Deserialize<Settings>(await File.ReadAllTextAsync(path))!;
        }
        catch (FileNotFoundException)
        {
            File.Create(path);
            return new Settings(null);
        }
    }
}