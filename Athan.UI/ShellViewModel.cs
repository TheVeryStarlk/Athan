using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Athan.UI;

internal sealed partial class ShellViewModel(TasbihViewModel tasbihViewModel, SettingsViewModel settingsViewModel) : ObservableObject
{
    public ObservableCollection<HeaderViewModel> Header { get; } =
    [
        new PrayersViewModel
        {
            Glyph = "🌃",
            Title = "Kuwait, Kuwait"
        },
        new PrayersViewModel
        {
            Glyph = "🌄",
            Title = "Amman, Jordan"
        },
        new PrayersViewModel
        {
            Glyph = "🌇",
            Title = "Paris, France"
        },
        new PrayersViewModel
        {
            Glyph = "🌆",
            Title = "Cairo, Egypt"
        },
        new PrayersViewModel
        {
            Glyph = "🌅",
            Title = "Istanbul, Turkey"
        },
        new PrayersViewModel
        {
            Glyph = "🌉",
            Title = "Dubai, UAE"
        },
        new PrayersViewModel
        {
            Glyph = "🏙️",
            Title = "New York, USA"
        },
        new PrayersViewModel
        {
            Glyph = "🌇",
            Title = "London, UK"
        },
        new PrayersViewModel
        {
            Glyph = "🌄",
            Title = "Jakarta, Indonesia"
        },
        new PrayersViewModel
        {
            Glyph = "🌃",
            Title = "Karachi, Pakistan"
        },
        new PrayersViewModel
        {
            Glyph = "🏙️",
            Title = "Musqat, Oman"
        },
        new PrayersViewModel
        {
            Glyph = "🌄",
            Title = "Moscow, Russia"
        },
        new PrayersViewModel
        {
            Glyph = "🌅",
            Title = "Tehran, Iran"
        }
    ];

    public ObservableCollection<FooterViewModel> Footer { get; } =
    [
        tasbihViewModel,
        settingsViewModel
    ];

    [ObservableProperty]
    public partial INotifyPropertyChanged? Current { get; set; }

    [RelayCommand]
    private void Initialize()
    {
        Current = Header[0];
    }
}

internal abstract partial class ItemViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Title { get; set; }
    
    [ObservableProperty]
    public partial string? Glyph { get; set; }
}

internal abstract class HeaderViewModel : ItemViewModel;

internal abstract class FooterViewModel : ItemViewModel;