using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
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

    public ObservableCollection<FooterViewModel> Footer { get; }

    [ObservableProperty]
    public partial INotifyPropertyChanged? Current { get; set; }

    public ShellViewModel(TasbihViewModel tasbihViewModel, SettingsViewModel settingsViewModel)
    {
        Footer =
        [
            tasbihViewModel,
            settingsViewModel
        ];

        WeakReferenceMessenger.Default.Register<ShellViewModel, DeleteMessage>(this, Delete);
    }

    private static void Delete(ShellViewModel recipient, DeleteMessage message)
    {
        var index = recipient.Header.IndexOf(message.Instance);

        recipient.Header.Remove(message.Instance);

        if (recipient.Header.Count is 0 && recipient.Current is HeaderViewModel)
        {
            recipient.Current = null;
            return;
        }

        if (recipient.Current?.Equals(message.Instance) ?? true)
        {
            recipient.Current = recipient.Header[Math.Min(index, recipient.Header.Count - 1)];
        }
    }

    [RelayCommand]
    private void Initialize()
    {
        Current = Header[0];
    }
}

internal sealed class DeleteMessage(HeaderViewModel instance)
{
    public HeaderViewModel Instance => instance;
}