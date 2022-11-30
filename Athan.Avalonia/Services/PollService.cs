using FluentResults;

namespace Athan.Avalonia.Services;

internal sealed class PollService
{
    private const int Threshold = 3;

    public async Task<Result<T>> HandleAsync<T>(Func<Task<Result<T>>> task)
    {
        var count = 0;

        var result = await task();

        while (Threshold > count)
        {
            count++;

            if (result.IsFailed)
            {
                result = await task();
                continue;
            }

            if (result.IsSuccess)
            {
                return Result.Ok(result.Value);
            }
        }

        return Result.Fail(result.Errors);
    }
}