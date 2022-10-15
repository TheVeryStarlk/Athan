using Athan.Avalonia.Contracts;

namespace Athan.Avalonia.Services;

internal sealed class NavigationService
{
    private readonly INavigable?[] stack = new INavigable?[2];

    public INavigable GoForward(INavigable navigable)
    {
        stack[1] ??= stack[0];
        stack[0] = navigable;
        return navigable;
    }

    public INavigable? GoBackward()
    {
        (stack[0], stack[1]) = (stack[1], stack[0]);
        return stack[0];
    }
}