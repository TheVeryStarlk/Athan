using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
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

    public LocationViewModel(LocationService locationService)
    {
        this.locationService = locationService;
    }

    [RelayCommand]
    private async Task GetLocationAsync()
    {
        var location = await locationService.GetLocationAsync();
        Message = $"Your location has been auto-detected to be in {location.City} {location.Country}.";
    }
}