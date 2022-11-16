using FluentResults;

namespace Athan.Avalonia.Extensions;

public static class ResultExtension
{
    public static void IfSuccess<TResult>(this Result<TResult> result, Action func)
    {
        if (result.IsSuccess)
        {
            func();
        }
    }

    public static void IfSuccess<TResult>(this Result<TResult> result, Func<Task> func)
    {
        if (result.IsSuccess)
        {
            func();
        }
    }
}