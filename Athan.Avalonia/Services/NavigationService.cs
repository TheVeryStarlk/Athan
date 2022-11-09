using System;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class NavigationService
{
    public event Action<INavigable?>? Navigated;

    private Setting? oldSetting;

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

        oldSetting = setting;
        Navigated?.Invoke(navigable);
        await navigable.Navigated(setting);
    }

    public async Task NavigateBackwardAsync()
    {
        (stack[0], stack[1]) = (stack[1], stack[0]);

        var navigable = stack[0];
        if (oldSetting is not null && navigable is not null)
        {
            await navigable.Navigated(oldSetting);
        }

        Navigated?.Invoke(stack[0]);
    }
}