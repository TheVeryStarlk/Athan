using System.Globalization;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Languages;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class DashboardViewModel : ObservableObject, INavigable
{
    public string Title => DateTime.Now.DayOfWeek switch
    {
        DayOfWeek.Sunday => Language.Sunday,
        DayOfWeek.Monday => Language.Monday,
        DayOfWeek.Tuesday => Language.Tuesday,
        DayOfWeek.Wednesday => Language.Wednesday,
        DayOfWeek.Thursday => Language.Thursday,
        DayOfWeek.Friday => Language.Friday,
        DayOfWeek.Saturday => Language.Saturday,
        _ => throw new ArgumentOutOfRangeException()
    };

    [ObservableProperty]
    private string? readableLocation;

    [ObservableProperty]
    private string? readableDate;

    [ObservableProperty]
    private PrayersViewModel prayersViewModel;

    private readonly SettingService settingService;

    private readonly NavigationService navigationService;

    public DashboardViewModel(PrayersViewModel prayersViewModel, SettingService settingService,
        NavigationService navigationService)
    {
        this.prayersViewModel = prayersViewModel;
        this.settingService = settingService;
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var setting = await settingService.ReadAsync();

        ReadableLocation = setting!.Location!.ToString();

        var calendar = new HijriCalendar();

        ReadableDate = DateTime.Parse($"{calendar.GetYear(DateTime.Now)}/" +
                                      $"{calendar.GetMonth(DateTime.Now)}/" +
                                      $"{calendar.GetDayOfMonth(DateTime.Now)}").ToShortDateString();

        await PrayersViewModel.InitializeAsync(setting.Location);
    }

    [RelayCommand]
    private void NavigateForward()
    {
        navigationService.NavigateForward(ViewModelLocator.SettingsViewModel);
    }
}