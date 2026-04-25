using System;
using System.Threading.Tasks;
using Athan.UI.Features.Search;
using Athan.UI.Features.Shell;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Empty;

internal sealed partial class EmptyViewModel : HeaderViewModel
{
    private readonly DialogService dialogService;
    private readonly GeopositionService geopositionService;
    private readonly LocationService locationService;

    public EmptyViewModel(DialogService dialogService, GeopositionService geopositionService, LocationService locationService)
    {
        this.dialogService = dialogService;
        this.geopositionService = geopositionService;
        this.locationService = locationService;

        Title = "Get started";
        Glyph = "🚀";
    }

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