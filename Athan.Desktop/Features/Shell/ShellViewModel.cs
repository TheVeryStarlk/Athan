using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Message { get; set; } = "Hello, world!";
}