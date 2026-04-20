using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.UI;

internal partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Icon { get; set; }

    [ObservableProperty]
    public partial string? Title { get; set; }
}