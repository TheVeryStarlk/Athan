using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class LocationViewModel : ObservableObject, INavigable
{
    public string Title => "Where are you";

    [ObservableProperty]
    private string message = "Hang tight...";

    [ObservableProperty]
    private Setting? setting;

    private readonly LocationService locationService;
    private readonly SettingsService settingsService;
    private readonly ThemeService themeService;
    private readonly NavigationService navigationService;

    public LocationViewModel(LocationService locationService, SettingsService settingsService,
        ThemeService themeService, NavigationService navigationService)
    {
        this.locationService = locationService;
        this.settingsService = settingsService;
        this.themeService = themeService;
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var location = await locationService.GetLocationAsync();
        Setting = await settingsService.UpdateAsync(new Setting(location, themeService.Theme));

        Message = $"Your location has been auto-detected to be in {location}.";
    }

    [RelayCommand]
    private async Task NavigateToDashboardAsync()
    {
        await navigationService.NavigateToAsync(ViewModelLocator.DashboardViewModel, Setting!);
    }
}