using System;
using System.Diagnostics;
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

        string[] main = ["Fajr", "Dhuhr", "Asr", "Maghrib", "Isha"];

        var now = DateTime.Now.Subtract(TimeSpan.FromDays(1));

        var filtered = prayers
            .Where(prayer => main.Contains(prayer.Key))
            .ToArray();

        Prayers = filtered
            .Select(prayer => new Prayer(prayer.Key, prayer.Value - now))
            .ToArray();

        var next = Prayers
            .Where(prayer => prayer.When.Ticks > 0)
            .OrderBy(prayer => prayer.When.Ticks)
            .First();

        Next = next;

        timer.Interval = next.When.TotalMilliseconds;
        timer.Enabled = true;
    }
}