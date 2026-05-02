using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Welcome;

internal sealed partial class WelcomeViewModel : HeaderViewModel
{
    private readonly DialogService dialogService;
    private readonly GeopositionService geopositionService;
    private readonly LocationService locationService;

    public WelcomeViewModel(DialogService dialogService, GeopositionService geopositionService, LocationService locationService)
    {
        this.dialogService = dialogService;
        this.geopositionService = geopositionService;
        this.locationService = locationService;

        Title = "Welcome";
        Glyph = "🚀";
        Deletable = false;
    }

    [RelayCommand]
    private async Task LocateAsync()
    {
        // Use proper mocks for debug.
        WeakReferenceMessenger.Default.Send(new AddMessage(new Location
        {
            Name = "Riyadh Region, Saudi Arabia",
            Latitude = 24.7136,
            Longitude = 46.6753
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