﻿using System.Collections.ObjectModel;
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

    private readonly PrayerService prayerService;
    private readonly PollyService pollyService;
    private readonly NotificationService notificationService;

    public PrayersViewModel(PrayerService prayerService, PollyService pollyService,
        NotificationService notificationService)
    {
        this.prayerService = prayerService;
        this.pollyService = pollyService;
        this.notificationService = notificationService;
    }

    public async Task InitializeAsync(Location location)
    {
        var prayers = await pollyService.HandleAsync(
            result => result is null,
            async () => await prayerService.GetTimingsAsync(location.City, location.Country));

        if (prayers is null)
        {
            return;
        }

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