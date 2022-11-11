using System;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class NavigationService
{
    public event Action<INavigable?>? Navigated;

    private Setting? loadedSetting;

    private readonly INavigable?[] stack = new INavigable?[2];

    public void NavigateTo(INavigable navigable)
    {
        stack[1] = stack[0];
        stack[0] = navigable;
        Navigated?.Invoke(navigable);
    }

    public async Task NavigateToAsync(INavigable navigable, Setting setting)
    {
        stack[1] = stack[0];
        stack[0] = navigable;

        loadedSetting = setting;
        await navigable.Navigated(setting);

        Navigated?.Invoke(navigable);
    }

    public async Task NavigateBackwardAsync()
    {
        (stack[0], stack[1]) = (stack[1], stack[0]);

        var navigable = stack[0];

        if (loadedSetting is not null && navigable is not null)
        {
            await navigable.Navigated(loadedSetting);
        }

        Navigated?.Invoke(stack[0]);
    }
}