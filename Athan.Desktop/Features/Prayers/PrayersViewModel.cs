using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Desktop.Features.Prayers;

public sealed partial class PrayersViewModel(PrayerService prayerService) : ObservableObject
{
    [ObservableProperty]
    public partial Prayer? NextPrayer { get; set; }

    [ObservableProperty]
    public partial Prayer[]? Prayers { get; set; }

    public async Task InitializeAsync()
    {
        NextPrayer = await prayerService.GetNextPrayerAsync();
        Prayers = await prayerService.GetPrayersAsync();
    }
}