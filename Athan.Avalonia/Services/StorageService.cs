using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;

namespace Athan.Avalonia.Services;

internal sealed class StorageService
{
    private readonly string storage = 
        Path.Combine(Environment.SpecialFolder.CommonApplicationData.ToString(), nameof(Athan));

    public async Task WriteAsync<T>(string key, T value) where T : notnull
    {
        var elements = await TryReadAllLinesAsync(storage);

        if (elements.Any(line => line == key))
        {
            var index = elements.IndexOf(key);
            elements[index + 1] = ConvertType<string>(value);

            await File.WriteAllLinesAsync(storage, elements);
        }
        else
        {
            var list = new List<string>(elements)
            {
                key,
                ConvertType<string>(value)
            };

            await File.WriteAllLinesAsync(storage, list);
        }
    }

    public async Task<T> ReadAsync<T>(string key)
    {
        var elements = await TryReadAllLinesAsync(storage);

        var index = elements.IndexOf(key);

        return ConvertType<T>(elements[index + 1]);
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