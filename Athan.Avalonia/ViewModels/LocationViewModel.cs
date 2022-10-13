using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class LocationViewModel : ObservableObject, INavigable
{
    public string Title => "Where do you live";

    [ObservableProperty]
    private bool hasGotLocation;

    [ObservableProperty]
    private string? message;

    private readonly LocationService locationService;

    public LocationViewModel(LocationService locationService)
    {
        this.locationService = locationService;
    }

    [RelayCommand]
    private async Task GetLocationAsync()
    {
        HasGotLocation = true;
        var location = await locationService.GetLocationAsync();
        Message = $"Your location has been auto-detected to be in {location.City} {location.Country}.";
    }
}