using System;
using System.Threading.Tasks;
using LightResults;

namespace Athan.Avalonia.Extensions;

internal static class TaskExtensions
{
    public static async Task<Result<T2>> ThenAsync<T, T2>(this Task<Result<T>> task, Func<T, Task<Result<T2>>> next)
    {
        var result = await task;
        return result.IsSuccess(out var value) ? await next(value) : result.AsFailure<T2>();
    }
}