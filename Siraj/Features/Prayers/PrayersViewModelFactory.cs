using System;
using Siraj.Features.Locations;
using Siraj.Features.Settings;

namespace Siraj.Features.Prayers;

internal sealed class PrayersViewModelFactory(SettingsService settingsService, TimerService timerService, TimeProvider timeProvider)
{
    public PrayersViewModel Create(Location location)
    {
        return new PrayersViewModel(settingsService, timerService, timeProvider, location);
    }
}