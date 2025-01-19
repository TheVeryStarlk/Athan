using System;
using System.Threading.Tasks;
using LightResults;
using Serilog;

namespace Athan.Avalonia.Extensions;

internal static class TaskExtensions
{
    public static async Task<Result<TValue>> ThenAsync<T, TValue>(this Task<Result<T>> task, Func<T, Task<Result<TValue>>> next)
    {
        var result = await task;

        if (!result.IsSuccess(out var value))
        {
            return result.AsFailure<TValue>();
        }

        return await next(value);
    }

    public static async void Await(this Task task)
    {
        try
        {
            await task;
        }
        catch (Exception exception)
        {
            Log.Error(exception, "An exception has occured.");
        }
    }
}