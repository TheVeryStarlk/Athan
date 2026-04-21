using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Athan.UI;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<TasbihViewModel> Footer { get; } = 
        [
        new TasbihViewModel()
        ];

    public ObservableCollection<PrayersViewModel> Header { get; } =
    [
        new PrayersViewModel
        {
            Emoji = "🌃",
            Title = "Kuwait, Kuwait"
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
        },
        new PrayersViewModel
        {
            Emoji = "🌆",
            Title = "Cairo, Egypt"
        },
        new PrayersViewModel
        {
            Emoji = "🌅",
            Title = "Istanbul, Turkey"
        },
        new PrayersViewModel
        {
            Emoji = "🌉",
            Title = "Dubai, UAE"
        },
        new PrayersViewModel
        {
            Emoji = "🏙️",
            Title = "New York, USA"
        },
        new PrayersViewModel
        {
            Emoji = "🌇",
            Title = "London, UK"
        },
        new PrayersViewModel
        {
            Emoji = "🌄",
            Title = "Jakarta, Indonesia"
        },
        new PrayersViewModel
        {
            Emoji = "🌃",
            Title = "Karachi, Pakistan"
        }
    ];
}