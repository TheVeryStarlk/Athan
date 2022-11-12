using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
    private readonly PollyService pollyService;
    private readonly NotificationService notificationService;

    public PrayersViewModel(PrayerService prayerService, PollyService pollyService,
        NotificationService notificationService)
    {
        this.prayerService = prayerService;
        this.pollyService = pollyService;
        this.notificationService = notificationService;

        notificationService.NotificationActivated += async () => await InitializeAsync(loadedLocation!);
    }

    public async Task InitializeAsync(Location location)
    {
        loadedLocation = location;

        var prayers = await pollyService.HandleAsync(
            result => result is null,
            async () => await prayerService.GetTimingsAsync(location.City, location.Country));

        TodayPrayers = new ObservableCollection<Prayer>(prayers!);

        var closest = prayerService.GetClosest(prayers!);
        NextPrayer = closest.Name;

        await notificationService.InitializeAsync();

        notificationService.ScheduleNotification(
            closest.Name,
            $"You have entered the prayer time for {closest.Name}.",
            closest.DateTime);
    }
}