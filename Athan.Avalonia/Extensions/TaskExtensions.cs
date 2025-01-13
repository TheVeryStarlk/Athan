using System;
using System.Threading.Tasks;
using LightResults;

namespace Athan.Avalonia.Extensions;

internal static class TaskExtensions
{
    public static async Task<Result> ThenAsync<T>(this Task<Result<T>> task, Func<T, Task<Result>> next)
    {
        var result = await task;

        if (!result.IsSuccess(out var value))
        {
            return result.AsFailure();
        }

        return await next(value);
    }
}