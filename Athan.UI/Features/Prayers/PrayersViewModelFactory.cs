using System;
using Athan.UI.Features.Locations;

namespace Athan.UI.Features.Prayers;

internal sealed class PrayersViewModelFactory(TimeProvider timeProvider)
{
    public PrayersViewModel Create(Location location)
    {
        return new PrayersViewModel(location, timeProvider);
    }
}
