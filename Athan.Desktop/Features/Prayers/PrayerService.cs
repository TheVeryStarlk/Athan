namespace Athan.Desktop.Features.Prayers;

public sealed class PrayerService
{
    public async Task<Prayer> GetNextPrayerAsync()
    {
        await Task.Delay(1000);

        return new Prayer("Fajar", "5:00 AM");
    }

    public async Task<Prayer[]> GetPrayersAsync()
    {
        await Task.Delay(1000);

        return
        [
            new Prayer("Fajar", "5:00 AM"),
            new Prayer("Duhur", "7:00 PM"),
            new Prayer("Asr", "10:00 PM"),
            new Prayer("Maghrib", "6:00 PM"),
            new Prayer("Isha", "9:00 PM")
        ];
    }
}