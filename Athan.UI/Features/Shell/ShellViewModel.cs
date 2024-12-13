using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Message { get; set; } = "Greetings!";

    [RelayCommand]
    private void Greet()
    {
        Message = "Hello!";
    }
}