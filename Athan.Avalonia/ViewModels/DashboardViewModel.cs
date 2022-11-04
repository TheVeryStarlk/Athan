using System;
using System.Globalization;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    private Setting? loadedSetting;

    private readonly NavigationService navigationService;

    public DashboardViewModel(PrayersViewModel prayersViewModel, NavigationService navigationService)
    {
        this.prayersViewModel = prayersViewModel;
        this.navigationService = navigationService;
    }

    public async Task Navigated(Setting setting)
    {
        loadedSetting = setting;

        ReadableLocation = setting.Location.ToString();

        var calendar = new HijriCalendar();

        ReadableDate = DateTime.Parse($"{calendar.GetYear(DateTime.Now)}/" +
                                      $"{calendar.GetMonth(DateTime.Now)}/" +
                                      $"{calendar.GetDayOfMonth(DateTime.Now)}").ToShortDateString();

        await PrayersViewModel.InitializeAsync(setting.Location);
    }

    [RelayCommand]
    private async Task NavigateToSettingsAsync()
    {
        await navigationService.GoForwardAsync(ViewModelLocator.SettingsViewModel, loadedSetting!);
    }
}