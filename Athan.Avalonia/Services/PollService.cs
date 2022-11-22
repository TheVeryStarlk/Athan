using Athan.Avalonia.Messages;
using CommunityToolkit.Mvvm.Messaging;
using FluentResults;

namespace Athan.Avalonia.Services;

internal sealed class PollService
{
    private const int Threshold = 3;

    public async Task<T?> HandleAsync<T>(Func<Task<Result<T>>> task)
    {
        var count = 0;

        var result = await task();

        while (Threshold > count)
        {
            count++;

            result = await task();

            if (result.IsSuccess)
            {
                return result.Value;
            }
        }

        WeakReferenceMessenger.Default.Send(new DialogRequestMessage("Oh no!", result.Errors[0].Message));
        return default;
    }
}