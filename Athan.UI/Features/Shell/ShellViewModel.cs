using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Athan.UI.Features.Empty;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<HeaderViewModel> Header { get; } =
    [
        // new PrayersViewModel
        // {
        //     Glyph = "🌃",
        //     Title = "Kuwait, Kuwait"
        // }
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

        WeakReferenceMessenger.Default.Register<ShellViewModel, AddMessage>(this, Add);
        WeakReferenceMessenger.Default.Register<ShellViewModel, DeleteMessage>(this, Delete);
    }

    private static void Add(ShellViewModel recipient, AddMessage message)
    {
        var instance = new PrayersViewModel
        {
            Title = Random.Shared.Next().ToString(),
            Glyph = "🌄"
        };

        recipient.Header.Add(instance);
        recipient.Current = instance;
    }

    private static void Delete(ShellViewModel recipient, DeleteMessage message)
    {
        recipient.Header.Remove(message.Instance);
        recipient.Current = recipient.Header.Count > 0 ? recipient.Header[0] : null;
    }

    [RelayCommand]
    private void Initialize()
    {
        // Current = Header[0];
    }
}

internal sealed class DeleteMessage(HeaderViewModel instance)
{
    public HeaderViewModel Instance => instance;
}