using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

namespace Athan.Avalonia.Services;

internal sealed class PollyService
{
    private readonly NavigationService navigationService;

    public PollyService(NavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    public async Task<T?> HandleAsync<T>(Func<T, bool> predicate, Func<Task<T>> task)
    {
        return await Policy
            .HandleResult(predicate)
            .RetryAsync(retryCount: 3, (_, count) =>
            {
                if (count == 3)
                {
                    navigationService.NavigateTo(ViewModelLocator.OfflineViewModel);
                }
            })
            .ExecuteAsync(async () => await task());
    }
}