using System.Collections.ObjectModel;
using Athan.Avalonia.Languages;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

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

        WeakReferenceMessenger.Default.Register<DialogTryAgainRequestMessage>(this,
            DialogTryAgainRequestMessageHandlerAsync);

        notificationService.NotificationActivated += async () => await InitializeAsync(loadedLocation!);
    }

    private async void DialogTryAgainRequestMessageHandlerAsync(object recipient, DialogTryAgainRequestMessage message)
    {
        if (message.Requester is not PrayersViewModel)
        {
            return;
        }

        await LoadPrayersAsync();
    }

    public async Task InitializeAsync(Location location)
    {
        loadedLocation = location;
        await LoadPrayersAsync();
    }

    private async Task LoadPrayersAsync()
    {
        if (TodayPrayers is not null)
        {
            return;
        }

        var prayers = await pollService.HandleAsync(async () =>
            await prayerService.GetTimingsAsync(loadedLocation!.City, loadedLocation.Country));

        if (prayers.IsFailed)
        {
            WeakReferenceMessenger.Default.Send(new DialogRequestMessage(this, prayers.Errors.ToArray()));
            return;
        }

        TodayPrayers = new ObservableCollection<Prayer>(prayers.Value!);

        var closest = prayerService.GetClosest(prayers.Value!);
        NextPrayer = closest.Name;

        await notificationService.InitializeAsync();

        notificationService.ScheduleNotification(
            closest.Name,
            $"{Language.YouHaveEnteredThePrayerTimeFor} {closest.Name}.",
            closest.DateTime);
    }
}