using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<HeaderViewModel> Header { get; }

    public ObservableCollection<FooterViewModel> Footer { get; }

    [ObservableProperty]
    public partial INotifyPropertyChanged? Current { get; set; }

    public ShellViewModel(TasbihViewModel tasbihViewModel, SettingsViewModel settingsViewModel)
    {
        Footer = [tasbihViewModel, settingsViewModel];

        Header =
        [
            CreateItem("🌃", "Kuwait, Kuwait"),
            CreateItem("🌄", "Amman, Jordan"),
            CreateItem("🌇", "Paris, France"),
            CreateItem("🌆", "Cairo, Egypt"),
            CreateItem("🌅", "Istanbul, Turkey"),
            CreateItem("🌉", "Dubai, UAE"),
            CreateItem("🏙️", "New York, USA"),
            CreateItem("🌇", "London, UK"),
            CreateItem("🌄", "Jakarta, Indonesia"),
            CreateItem("🌃", "Karachi, Pakistan"),
            CreateItem("🏙️", "Muscat, Oman"),
            CreateItem("🌄", "Moscow, Russia"),
            CreateItem("🌅", "Tehran, Iran")
        ];
    }

    private PrayersViewModel CreateItem(string glyph, string title)
    {
        return new PrayersViewModel
        {
            Glyph = glyph,
            Title = title,
            OnDelete = DeleteItem
        };
    }

    private void DeleteItem(HeaderViewModel item)
    {
        var index = Header.IndexOf(item);

        Header.Remove(item);

        if (Equals(Current, item))
        {
            Current = Header.Count > 0 ? Header[Math.Min(index, Header.Count - 1)] : null;
        }
    }

    [RelayCommand]
    private void Initialize()
    {
        Current = Header[0];
    }
}
