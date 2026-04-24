using System;
using System.Threading.Tasks;
using Athan.UI.Features.Search;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Empty;

internal sealed partial class EmptyViewModel(DialogService dialogService, GeopositionService geopositionService, LocationService locationService) : ObservableObject
{
    [RelayCommand]
    private async Task Foo()
    {
        if (!await geopositionService.IsAllowedAsync())
        {
            await dialogService.ShowMessageAsync(
                "Where are you?",
                $"Please enable location access for Athan{Environment.NewLine}"
                + "You can also search manually for a location");
        }
        else
        {
            var geoposition = await geopositionService.GetAsync();
            var location = await locationService.ReverseAsync(geoposition.Latitude, geoposition.Longitude);

            WeakReferenceMessenger.Default.Send(new AddMessage(location));
        }
    }
}