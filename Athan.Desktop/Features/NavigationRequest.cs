namespace Athan.Desktop.Features;

internal sealed record NavigationRequest(Destination Destination);

internal enum Destination
{
    Welcome,
    Setting
}