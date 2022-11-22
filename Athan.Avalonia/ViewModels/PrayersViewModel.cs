using System.Collections.ObjectModel;
using Athan.Avalonia.Services;
using Athan.Services;
using Athan.Services.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    private string? nextPrayer;

    [ObservableProperty]
    private ObservableCollection<Prayer>? todayPrayers;

    private Location? loadedLocation;

    private readonly PrayerService prayerService;
    private readonly PollService pollService;
    private readonly NotificationService notificationService;

    public PrayersViewModel(PrayerService prayerService, PollService pollService,
        NotificationService notificationService)
    {
        this.prayerService = prayerService;
        this.pollService = pollService;
        this.notificationService = notificationService;

        notificationService.NotificationActivated += async () => await InitializeAsync(loadedLocation!);
    }

    public async Task InitializeAsync(Location location)
    {
        loadedLocation = location;

        var prayers = await pollService.HandleAsync(async () =>
            await prayerService.GetTimingsAsync(location.City, location.Country));

        if (prayers is null)
        {
            return;
        }

        TodayPrayers = new ObservableCollection<Prayer>(prayers);

        var closest = prayerService.GetClosest(prayers);
        NextPrayer = closest.Name;

        await notificationService.InitializeAsync();

        notificationService.ScheduleNotification(
            closest.Name,
            $"You have entered the prayer time for {closest.Name}.",
            closest.DateTime);
    }
}