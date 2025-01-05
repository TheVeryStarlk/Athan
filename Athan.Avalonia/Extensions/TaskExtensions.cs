using System;
using System.Threading.Tasks;
using LightResults;

namespace Athan.Avalonia.Extensions;

internal static class TaskExtensions
{
    public static async Task Match<T>(this Task<Result<T>> task, Func<T, Task> success, Action<Result> failure)
    {
        try
        {
            var result = await task;

            if (!result.IsSuccess(out var value))
            {
                failure(Result.Failure(result.Errors));
            }
            else
            {
                await success(value);
            }
        }
        catch (Exception exception)
        {
            failure(Result.Failure(exception.Message));
        }
    }
}