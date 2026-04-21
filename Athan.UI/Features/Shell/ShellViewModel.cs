using System.Collections.ObjectModel;
using System.ComponentModel;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Shell;

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