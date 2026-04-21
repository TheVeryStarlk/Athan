using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Athan.UI;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<ItemViewModel> Footer { get; } =
    [
        new TasbihViewModel()
    ];

    public ObservableCollection<ItemViewModel> Header { get; } =
    [
        new PrayersViewModel
        {
            Icon = "🌃",
            Title = "Kuwait, Kuwait"
        },
        new PrayersViewModel
        {
            Icon = "🌄",
            Title = "Amman, Jordan"
        },
        new PrayersViewModel
        {
            Icon = "🌇",
            Title = "Paris, France"
        },
        new PrayersViewModel
        {
            Icon = "🌆",
            Title = "Cairo, Egypt"
        },
        new PrayersViewModel
        {
            Icon = "🌅",
            Title = "Istanbul, Turkey"
        },
        new PrayersViewModel
        {
            Icon = "🌉",
            Title = "Dubai, UAE"
        },
        new PrayersViewModel
        {
            Icon = "🏙️",
            Title = "New York, USA"
        },
        new PrayersViewModel
        {
            Icon = "🌇",
            Title = "London, UK"
        },
        new PrayersViewModel
        {
            Icon = "🌄",
            Title = "Jakarta, Indonesia"
        },
        new PrayersViewModel
        {
            Icon = "🌃",
            Title = "Karachi, Pakistan"
        },
        new PrayersViewModel
        {
            Icon = "🏙️",
            Title = "Musqat, Oman"
        },
        new PrayersViewModel
        {
            Icon = "🌄",
            Title = "Moscow, Russia"
        },
        new PrayersViewModel
        {
            Icon = "🌅",
            Title = "Tehran, Iran"
        }
    ];
}

internal abstract partial class ItemViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Icon { get; set; }

    [ObservableProperty]
    public partial string? Title { get; set; }
}