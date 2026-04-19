using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Athan.UI;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<PrayersViewModel> Items { get; } = 
    [
        new PrayersViewModel
        {
            Emoji = "🌃",
            Title = "Riyadh, Saudi Arabia"
        },
        new PrayersViewModel
        {
            Emoji = "🌄",
            Title = "Amman, Jordan"
        },
        new PrayersViewModel
        {
            Emoji = "🌇",
            Title = "Paris, France"
        }
    ];
}