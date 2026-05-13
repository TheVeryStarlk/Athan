using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Siraj.Features.Welcome;

internal sealed class GeopositionService
{
    public async Task<bool> IsAllowedAsync()
    {
        var status = await Geolocator.RequestAccessAsync();
        return status is GeolocationAccessStatus.Allowed;
    }

    public async Task<Geoposition> GetAsync()
    {
        var geolocator = new Geolocator();
        var result = await geolocator.GetGeopositionAsync();

        return new Geoposition(result.Coordinate.Latitude, result.Coordinate.Longitude);
    }
}

internal readonly struct Geoposition(double latitude, double longitude)
{
    public double Latitude => latitude;

    public double Longitude => longitude;
}