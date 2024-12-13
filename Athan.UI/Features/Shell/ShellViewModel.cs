using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Message { get; set; } = "Greetings!";

    private int count;

    [RelayCommand]
    private void Greet()
    {
        count++;

        var result = new StringBuilder("Hell")
            .Append(new string('o', count))
            .Append('!')
            .ToString();

        Message = result;
    }
}