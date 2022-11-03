using System.Threading.Tasks;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    private string? nextPrayer;

    private readonly PrayerService prayerService;
    private readonly NotificationService notificationService;

    public PrayersViewModel(PrayerService prayerService, NotificationService notificationService)
    {
        this.prayerService = prayerService;
        this.notificationService = notificationService;
    }

    public async Task InitializeAsync(Location location)
    {
        await notificationService.InitializeAsync();

        var prayers = await prayerService.GetTimingsAsync(location.City, location.Country);
        var closest = prayerService.GetClosest(prayers);

        NextPrayer = closest.Name;

        await notificationService.ScheduleNotificationAsync(
            closest.Name,
            $"You have entered the prayer time for {closest.Name}.",
            closest.Time.TimeOfDay);
    }
}