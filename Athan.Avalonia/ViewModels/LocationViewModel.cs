using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using Athan.Services;
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
    private readonly PollyService pollyService;
    private readonly ThemeService themeService;
    private readonly NavigationService navigationService;

    public LocationViewModel(LocationService locationService, SettingsService settingsService,
        PollyService pollyService, ThemeService themeService, NavigationService navigationService)
    {
        this.locationService = locationService;
        this.settingsService = settingsService;
        this.pollyService = pollyService;
        this.themeService = themeService;
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var location = await pollyService.HandleAsync(
            result => result is null,
            async () => await locationService.GetLocationAsync());

        if (location is null)
        {
            return;
        }

        Setting = await settingsService.UpdateAsync(new Setting(location, themeService.Theme));
        Message = $"Your location has been auto-detected to be in {location}.";
    }

    [RelayCommand]
    private async Task NavigateToDashboardAsync()
    {
        await navigationService.NavigateToAsync(ViewModelLocator.DashboardViewModel, Setting!);
    }
}