using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;
using Athan.UI.Features.Locations;

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
        Deletable = false;
    }

    [RelayCommand]
    private async Task LocateAsync()
    {
        // Use proper mocks for debug.
        WeakReferenceMessenger.Default.Send(new AddMessage(new Location
        {
            Name = "Foo",
            Latitude = 0,
            Longitude = 0
        }));

        return;

        if (!await geopositionService.IsAllowedAsync())
        {
            await dialogService.ShowMessageAsync("Where are you?", "Make sure location access is enabled in your system");
        }
        else
        {
            var geoposition = await geopositionService.GetAsync();
            var location = await locationService.ReverseAsync(geoposition.Latitude, geoposition.Longitude);

            WeakReferenceMessenger.Default.Send(new AddMessage(location));
        }
    }
}