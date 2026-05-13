using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Windows.Foundation.Collections;
using Windows.Storage;
using Siraj.Features.Locations;
using Siraj.Features.Prayers.Calculation;

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

    public CallReciter Reciter
    {
        get => Get(CallReciter.MisharyAlAfasy, SirajSerializerContext.Default.CallReciter);
        set => Set(value, SirajSerializerContext.Default.CallReciter);
    }

    public PrayerCalculation Calculation
    {
        get => Get(PrayerCalculation.Makkah, SirajSerializerContext.Default.PrayerCalculation);
        set => Set(value, SirajSerializerContext.Default.PrayerCalculation);
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