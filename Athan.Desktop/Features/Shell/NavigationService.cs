using Serilog;

namespace Athan.Desktop.Features.Shell;

public sealed class NavigationService(ILogger logger)
{
    public event Action<Destination>? Navigated;

    private readonly Destination[] stack = new Destination[2];

    public void Navigate(Destination destination)
    {
        stack[1] = stack[0];
        stack[0] = destination;

        Navigated?.Invoke(destination);

        logger.Debug("Navigated to: {Destination}", destination);
    }

    public void NavigateBackward()
    {
        (stack[0], stack[1]) = (stack[1], stack[0]);
        Navigated?.Invoke(stack[0]);

        logger.Debug("Navigated backward to: {Destination}", stack[0]);
    }
}

public enum Destination
{
    Welcome,
    Prayers,
    Settings,
    Offline
}