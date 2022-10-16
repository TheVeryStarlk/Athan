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

    private readonly LocationService locationService;
    private readonly SettingsService settingsService;

    public LocationViewModel(LocationService locationService, SettingsService settingsService)
    {
        this.locationService = locationService;
        this.settingsService = settingsService;
    }

    [RelayCommand]
    private async Task GetLocationAsync()
    {
        var (city, country) = await locationService.GetLocationAsync();
        await settingsService.UpdateAsync(new Settings(new Location(city, country)));

        Message = $"Your location has been auto-detected to be in {city} {country}.";
    }
}