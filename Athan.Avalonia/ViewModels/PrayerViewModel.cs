using System;
using System.Text;
using System.Threading.Tasks;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayerViewModel(PrayerService prayerService) : ObservableObject
{
    [ObservableProperty]
    public partial string Result { get; set; } = "Click the button!";

    [RelayCommand]
    public async Task InitializeAsync()
    {
        Result = "Please wait...";

        var result = await prayerService.GetAsync("Saudi Arabia", "Riyadh");

        if (!result.IsSuccess(out var timings))
        {
            Result = "Could not get timings!";
            return;
        }

        var builder = new StringBuilder();

        var now = DateTime.Now;

        foreach (var pair in timings)
        {
            var offset = now.Add(pair.Value);

            builder.AppendLine($"{pair.Key} @ {offset:t}. After {(int) offset.Subtract(now).TotalHours} hours.");
        }

        Result = builder.ToString();
    }
}