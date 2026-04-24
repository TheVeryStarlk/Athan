using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Shell.Messages;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<HeaderViewModel> Header { get; } =
    [
        new PrayersViewModel
        {
            Glyph = "🌃",
            Title = "Riyadh, Saudi Arabia"
        }
    ];

    public ObservableCollection<FooterViewModel> Footer { get; }

    [ObservableProperty]
    public partial ItemViewModel? Current { get; set; }

    private readonly INavigationService navigationService;

    public ShellViewModel(INavigationService navigationService, TasbihViewModel tasbihViewModel, SettingsViewModel settingsViewModel)
    {
        this.navigationService = navigationService;

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
        Navigate(Header[0]);
    }

    [RelayCommand]
    private void Navigate(ItemViewModel? selection)
    {
        Current = selection;
        navigationService.Navigate(Current);
    }
}