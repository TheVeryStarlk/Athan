namespace Athan.Desktop.Features.Shell;

internal sealed record NavigationRequest(Destination Destination);

internal enum Destination
{
    Welcome,
    Setting
}