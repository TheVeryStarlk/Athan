using System;
using Athan.UI.Features.Locations;
using Athan.UI.Features.Settings;

namespace Athan.UI.Features.Prayers;

internal sealed class PrayersViewModelFactory(SettingsService settingsService, TimerService timerService, TimeProvider timeProvider)
{
    public PrayersViewModel Create(Location location)
    {
        return new PrayersViewModel(location, settingsService, timerService, timeProvider);
    }
}