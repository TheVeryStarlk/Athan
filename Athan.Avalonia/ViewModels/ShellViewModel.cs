using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed class ShellViewModel : ObservableObject
{
    public string Greeting { get; } = "Welcome to Avalonia!";
}