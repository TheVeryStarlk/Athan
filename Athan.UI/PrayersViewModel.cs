using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.UI;

internal sealed partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Emoji { get; set; }

    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial string? Family { get; set; }
}