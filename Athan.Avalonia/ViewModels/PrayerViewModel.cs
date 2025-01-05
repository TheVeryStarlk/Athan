using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Athan.Avalonia.Models;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayerViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Prayer[] Prayers { get; set; } = [];

    [ObservableProperty]
    public partial Prayer? Next { get; set; }

    private readonly PrayerService prayerService;
    private readonly Timer timer = new();

    public PrayerViewModel(PrayerService prayerService)
    {
        this.prayerService = prayerService;
        timer.Elapsed += async (_, _) => await InitializeAsync();
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var result = await prayerService.GetAsync("Saudi Arabia", "Riyadh");

        if (!result.IsSuccess(out var prayers))
        {
            return;
        }

        Prayers = prayers
            .Select(prayer => new Prayer(prayer.Key, prayer.Value))
            .ToArray();

        var next = prayers
            .Where(prayer => prayer.Value.Ticks > 0)
            .OrderBy(prayer => prayer.Value.Ticks)
            .First();

        Next = new Prayer(next.Key, next.Value);

        timer.Interval = next.Value.TotalMilliseconds;
        timer.Enabled = true;
    }
}