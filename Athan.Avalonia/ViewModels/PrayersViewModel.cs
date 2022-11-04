using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    private string? nextPrayer;

    [ObservableProperty]
    private ObservableCollection<Prayer>? todayPrayers;

    private readonly PrayerService prayerService;
    private readonly NotificationService notificationService;

    public PrayersViewModel(PrayerService prayerService, NotificationService notificationService)
    {
        this.prayerService = prayerService;
        this.notificationService = notificationService;
    }

    public async Task InitializeAsync(Location location)
    {
        var prayers = await prayerService.GetTimingsAsync(location.City, location.Country);
        TodayPrayers = new ObservableCollection<Prayer>(prayers);

        var closest = prayerService.GetClosest(prayers);
        NextPrayer = closest.Name;

        await notificationService.InitializeAsync();

        await notificationService.ScheduleNotificationAsync(
            closest.Name,
            $"You have entered the prayer time for {closest.Name}.",
            closest.DateTime.ToLocalTime().TimeOfDay);
    }
}