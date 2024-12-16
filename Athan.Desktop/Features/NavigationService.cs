namespace Athan.Desktop.Features;

public sealed class NavigationService
{
    public event Action<Destination>? Navigated;

    public void Navigate(Destination destination)
    {
        Navigated?.Invoke(destination);
    }
}

public enum Destination
{
    Welcome,
    Settings
}