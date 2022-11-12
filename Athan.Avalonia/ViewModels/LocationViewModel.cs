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
    private bool canContinue;

    private readonly LocationService locationService;
    private readonly SettingService settingService;
    private readonly PollyService pollyService;
    private readonly ThemeService themeService;
    private readonly NavigationService navigationService;

    public LocationViewModel(LocationService locationService, SettingService settingService,
        PollyService pollyService, ThemeService themeService, NavigationService navigationService)
    {
        this.locationService = locationService;
        this.settingService = settingService;
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

        settingService.Update(new Setting(location!, themeService.Theme));
        Message = $"You seem to be in {location}.";
        CanContinue = true;
    }

    [RelayCommand]
    private void NavigateForward()
    {
        navigationService.NavigateForward(ViewModelLocator.DashboardViewModel);
    }
}