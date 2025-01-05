using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Athan.Avalonia.Models;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayerViewModel(PrayerService prayerService) : ObservableObject
{
    [ObservableProperty]
    public partial Prayer[] Prayers { get; set; } = [];

    private Timer? timer;

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
            .ToArray();

        timer = new Timer
        {
            AutoReset = true,
            Enabled = true,
            Interval = next[0].Value.TotalMicroseconds
        };
    }
}