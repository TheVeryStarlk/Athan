using System;
using Siraj.Features.Locations;
using Siraj.Features.Prayers.Voices;
using Siraj.Features.Settings;

namespace Siraj.Features.Prayers;

internal sealed class PrayersViewModelFactory(SettingsService settingsService, TimerService timerService, VoiceService voiceService, TimeProvider timeProvider)
{
    public PrayersViewModel Create(Location location)
    {
        return new PrayersViewModel(settingsService, timerService, voiceService, timeProvider, location);
    }
}