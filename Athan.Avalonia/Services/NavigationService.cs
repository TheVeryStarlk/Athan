using Athan.Avalonia.Contracts;

namespace Athan.Avalonia.Services;

internal sealed class NavigationService
{
    public event Action<INavigable?>? Navigated;

    private readonly INavigable?[] stack = new INavigable?[2];

    public void NavigateForward(INavigable navigable)
    {
        stack[1] = stack[0];
        stack[0] = navigable;
        Navigated?.Invoke(navigable);
    }

    public void NavigateBackward()
    {
        (stack[0], stack[1]) = (stack[1], stack[0]);
        Navigated?.Invoke(stack[0]);
    }
}