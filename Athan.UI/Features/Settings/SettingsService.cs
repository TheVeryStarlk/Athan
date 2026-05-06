using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace Athan.UI.Features.Settings;

internal sealed class SettingsService
{
    private readonly IPropertySet storage = ApplicationData.Current.LocalSettings.Values;

    public T Get<T>(T fallback, JsonTypeInfo<T> typeInfo)
    {
        if (!storage.TryGetValue(typeInfo.Type.Name, out var value))
        {
            return fallback;
        }

        var result = JsonSerializer.Deserialize((string) value, typeInfo);

        ArgumentNullException.ThrowIfNull(result);

        return result;
    }
    
    public void Set<T>(T value, JsonTypeInfo<T> typeInfo)
    {
        var serialized = JsonSerializer.Serialize(value, typeInfo);
        storage[typeInfo.Type.Name] = serialized;
    }
}