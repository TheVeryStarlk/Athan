using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Athan.Services;

public sealed class StorageService
{
    private Dictionary<string, string?> settings = [];

    private readonly string file = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Athan");

    public void Initialize()
    {
        Dictionary<string, string?>? result;

        try
        {
            var content = File.ReadAllText(file);
            result = JsonSerializer.Deserialize<Dictionary<string, string?>>(content);
        }
        catch
        {
            File.WriteAllText(file, string.Empty);
            result = [];
        }

        settings = result ?? [];
    }

    public void Save()
    {
        File.WriteAllText(file, JsonSerializer.Serialize(settings));
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