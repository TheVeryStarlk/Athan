using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;

namespace Athan.Avalonia.Services;

internal sealed class StorageService
{
    private readonly string path = Path.Combine(Environment.SpecialFolder.CommonApplicationData.ToString(), "Athan");

    public async Task WriteAsync<T>(string key, T value) where T : notnull
    {
        var storage = await TryReadAllLinesAsync(path);

        if (storage.Any(line => line == key))
        {
            var index = storage.IndexOf(key);
            storage[index] = ConvertType<string>(value);

            await File.WriteAllLinesAsync(path, storage);
        }
        else
        {
            var list = new List<string>(storage)
            {
                key,
                ConvertType<string>(value)
            };

            await File.WriteAllLinesAsync(path, list);
        }
    }

    public async Task<T> ReadAsync<T>(string key)
    {
        var storage = await TryReadAllLinesAsync(path);

        var index = storage.IndexOf(key);

        return ConvertType<T>(storage[index + 1]);
    }

    private async Task<string[]> TryReadAllLinesAsync(string path)
    {
        try
        {
            return await File.ReadAllLinesAsync(path);
        }
        catch (FileNotFoundException)
        {
            return Array.Empty<string>();
        }
    }
    
    private T ConvertType<T>(object value)
    {
        return (T) Convert.ChangeType(value, typeof(T));
    }
}