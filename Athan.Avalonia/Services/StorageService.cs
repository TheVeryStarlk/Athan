using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

namespace Athan.Avalonia.Services;

internal sealed class StorageService
{
    private Dictionary<string, string?> settings = [];

    private readonly string folder = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Athan");

    public void Initialize()
    {
        var file = Path.Join(folder, "Storage");

        if (!File.Exists(folder))
        {
            Directory.CreateDirectory(folder);
            File.WriteAllText(file, string.Empty);
        }

        var content = File.ReadAllText(file);

        Dictionary<string, string?>? result;

        try
        {
            result = JsonSerializer.Deserialize<Dictionary<string, string?>>(content);
        }
        catch
        {
            result = [];
        }

        settings = result ?? [];
    }

    public void Save()
    {
        File.WriteAllText(folder, JsonSerializer.Serialize(settings));
    }

    public bool TryGet<T>(string key, [NotNullWhen(true)] out T? value)
    {
        if (settings.TryGetValue(key, out var result))
        {
            value = JsonSerializer.Deserialize<T>(result!);
            return value is not null;
        }

        value = default;
        return false;
    }

    public void Set<T>(string key, T? value)
    {
        settings[key] = JsonSerializer.Serialize(value);
    }
}