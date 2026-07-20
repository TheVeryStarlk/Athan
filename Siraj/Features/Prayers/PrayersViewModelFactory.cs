using System;
using Siraj.Features.Locations;
using Siraj.Features.Prayers.Voices;
using Siraj.Features.Settings;
using Serilog;

namespace Siraj.Features.Prayers;

internal sealed class PrayersViewModelFactory(
    SettingsService settingsService,
    NotificationService notificationService,
    TimerService timerService,
    VoiceService voiceService,
    TimeProvider timeProvider)
{
    public PrayersViewModel Create(Location location)
    {
        Log.Debug("Creating prayer view model for {LocationName}", location.Name);
        return new PrayersViewModel(settingsService, notificationService, timerService, voiceService, timeProvider, location);
    }
}