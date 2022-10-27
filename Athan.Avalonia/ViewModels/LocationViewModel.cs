using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class LocationViewModel : ObservableObject, INavigable
{
    public string Title => "Where are you";

    [ObservableProperty]
    private string message = "Hang tight...";

    [ObservableProperty]
    private Settings? settings;

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
        var location = await locationService.GetLocationAsync();
        Settings = await settingsService.UpdateAsync(new Settings(location));

        Message = $"Your location has been auto-detected to be in {location}.";
    }

    [RelayCommand]
    private void Continue()
    {
        WeakReferenceMessenger.Default.Send(new NavigationRequestedMessage(nameof(DashboardViewModel), settings));
    }
}