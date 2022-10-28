using System.Threading.Tasks;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayersViewModel : ObservableObject
{
    public Location? Location { get; set; }

    [ObservableProperty]
    private string? closestPrayer;

    private readonly PrayerService prayerService;
    private readonly NotificationService notificationService;

    public PrayersViewModel(PrayerService prayerService, NotificationService notificationService)
    {
        this.prayerService = prayerService;
        this.notificationService = notificationService;

        WeakReferenceMessenger.Default.Register<ActivatedMessage>(this, ActivatedMessageHandler);
    }

    [RelayCommand]
    private async Task InitializedAsync()
    {
        if (Location is null)
        {
            return;
        }

        await notificationService.InitializeAsync();

        var prayers = await prayerService.GetTimingsAsync(Location.City, Location.Country);
        var closest = prayerService.GetClosest(prayers);

        ClosestPrayer = closest.Name;

        await notificationService.ScheduleNotificationAsync(
            closest.Name,
            $"You have entered the prayer time for {closest.Name}",
            closest.Time.TimeOfDay);
    }

    private async void ActivatedMessageHandler(object recipient, ActivatedMessage message)
    {
        await InitializedAsync();
    }
}