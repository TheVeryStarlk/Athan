using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Windows.Foundation.Collections;
using Windows.Storage;
using Siraj.Features.Locations;
using Siraj.Features.Prayers.Calculation;
using Siraj.Features.Prayers.Voices;

namespace Siraj.Features.Settings;

internal sealed class SettingsService
{
    public Location[] Locations
    {
        get => Get([], SirajSerializerContext.Default.LocationArray);
        set => Set(value, SirajSerializerContext.Default.LocationArray);
    }

    public Theme Theme
    {
        get => Get(Theme.System, SirajSerializerContext.Default.Theme);
        set => Set(value, SirajSerializerContext.Default.Theme);
    }

    public Voice Voice
    {
        get => Get(Voice.MisharyAlAfasy, SirajSerializerContext.Default.Voice);
        set => Set(value, SirajSerializerContext.Default.Voice);
    }

    public Method Method
    {
        get => Get(Method.Makkah, SirajSerializerContext.Default.Method);
        set => Set(value, SirajSerializerContext.Default.Method);
    }

    public bool Startup
    {
        get => Get(false, SirajSerializerContext.Default.Boolean, nameof(Startup));
        set => Set(value, SirajSerializerContext.Default.Boolean, nameof(Startup));
    }

    private readonly IPropertySet storage = ApplicationData.Current.LocalSettings.Values;

    private T Get<T>(T fallback, JsonTypeInfo<T> typeInfo, string? name = null)
    {
        if (!storage.TryGetValue(name ?? typeInfo.Type.Name, out var value))
        {
            return fallback;
        }

        return JsonSerializer.Deserialize((string) value, typeInfo) ?? fallback;
    }

    private void Set<T>(T value, JsonTypeInfo<T> typeInfo, string? name = null)
    {
        storage[name ?? typeInfo.Type.Name] = JsonSerializer.Serialize(value, typeInfo);
    }
}