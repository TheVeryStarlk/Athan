using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Athan.UI;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<HeaderViewModel> Header { get; } =
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
        },
        new PrayersViewModel
        {
            Emoji = "🏙️",
            Title = "Musqat, Oman"
        },
        new PrayersViewModel
        {
            Emoji = "🌄",
            Title = "Moscow, Russia"
        },
        new PrayersViewModel
        {
            Emoji = "🌅",
            Title = "Tehran, Iran"
        }
    ];

    public ObservableCollection<FooterViewModel> Footer { get; } =
    [
        new TasbihViewModel()
    ];
}

internal abstract partial class ItemViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Title { get; set; }
}

internal abstract partial class HeaderViewModel : ItemViewModel
{
    [ObservableProperty]
    public partial string? Emoji { get; set; }
}

internal abstract partial class FooterViewModel : ItemViewModel
{
    [ObservableProperty]
    public partial string? Icon { get; set; }
}