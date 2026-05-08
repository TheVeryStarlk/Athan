using System;
using Athan.UI.Features.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI.Features.Prayers;

internal sealed class PrayersViewModelFactory(TimeProvider timeProvider, IServiceProvider services)
{
    public PrayersViewModel Create(Location location)
    {
        return new PrayersViewModel(location, timeProvider, services.GetRequiredService<TimerService>());
    }
}
