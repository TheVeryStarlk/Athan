using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.Features.Shell;

internal sealed class ShellViewModel : ObservableObject
{
    public string Greeting => "Welcome to Avalonia!";
}