using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;

namespace Athan.Avalonia.Services;

internal sealed class NavigationService
{
    private readonly INavigable?[] stack = new INavigable?[2];

    public INavigable GoForward(INavigable navigable, Settings? settings = null)
    {
        stack[1] ??= stack[0];
        stack[0] = navigable;

        if (settings is not null)
        {
            navigable.Navigated(settings);
        }

        return navigable;
    }

    public INavigable? GoBackward()
    {
        (stack[0], stack[1]) = (stack[1], stack[0]);
        return stack[0];
    }
}