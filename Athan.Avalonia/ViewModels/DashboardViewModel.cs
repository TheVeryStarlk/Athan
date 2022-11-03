using System;
using System.Globalization;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class DashboardViewModel : ObservableObject, INavigable
{
    public string Title => DateTime.Now.DayOfWeek.ToString();

    [ObservableProperty]
    private string? readableLocation;

    [ObservableProperty]
    private string? readableDate;

    [ObservableProperty]
    private PrayersViewModel prayersViewModel;

    public DashboardViewModel(PrayersViewModel prayersViewModel)
    {
        this.prayersViewModel = prayersViewModel;
    }

    public async Task Navigated(Setting setting)
    {
        ReadableLocation = setting.Location.ToString();

        var calendar = new HijriCalendar();

        ReadableDate = DateTime.Parse($"{calendar.GetYear(DateTime.Now)}/" +
                              $"{calendar.GetMonth(DateTime.Now)}/" +
                              $"{calendar.GetDayOfMonth(DateTime.Now)}").ToShortDateString();

        await PrayersViewModel.InitializeAsync(setting.Location);
    }
}