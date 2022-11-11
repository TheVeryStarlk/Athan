using System;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class NavigationService
{
    public event Action<INavigable?>? Navigated;

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

        Navigated?.Invoke(navigable);
        await navigable.Navigated(setting);
    }

    public void NavigateBackward()
    {
        (stack[0], stack[1]) = (stack[1], stack[0]);
        Navigated?.Invoke(stack[0]);
    }
}