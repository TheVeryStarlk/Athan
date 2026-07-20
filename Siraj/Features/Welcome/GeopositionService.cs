using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Serilog;

namespace Siraj.Features.Welcome;

internal sealed class GeopositionService
{
    public async Task<bool> IsAllowedAsync()
    {
        var status = await Geolocator.RequestAccessAsync();

        Log.Information("Location permission request returned {PermissionStatus}", status);

        return status is GeolocationAccessStatus.Allowed;
    }

    public async Task<Geoposition> GetAsync()
    {
        Log.Debug("Requesting the current position");

        try
        {
            var geolocator = new Geolocator();
            var result = await geolocator.GetGeopositionAsync();

            Log.Debug("Current position acquired");

            return new Geoposition(result.Coordinate.Latitude, result.Coordinate.Longitude);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "Could not acquire the current position");
            throw;
        }
    }
}

internal readonly struct Geoposition(double latitude, double longitude)
{
    public double Latitude => latitude;

    public double Longitude => longitude;
}