using Athan.UI.Features.Empty;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<HeaderViewModel> Header { get; } = [];

    public ObservableCollection<FooterViewModel> Footer { get; }

    [ObservableProperty]
    public partial ItemViewModel? Current { get; set; }

    private readonly INavigationService navigationService;
    private readonly EmptyViewModel emptyViewModel;

    public ShellViewModel(
        INavigationService navigationService, 
        EmptyViewModel emptyViewModel, 
        TasbihViewModel tasbihViewModel, 
        SettingsViewModel settingsViewModel)
    {
        this.navigationService = navigationService;
        this.emptyViewModel = emptyViewModel;

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
            Title = message.Location.Name,
            Glyph = "🌄"
        };

        recipient.Header.Add(instance);
        recipient.Current = instance;

        if (recipient.Header.Count > 1)
        {
            recipient.Header.RemoveAt(0);
        }
    }

    private static void Delete(ShellViewModel recipient, DeleteMessage message)
    {
        recipient.Header.Remove(message.Instance);
        recipient.Current = recipient.Header.Count > 0 ? recipient.Header[0] : null;

        if (recipient.Header.Count is 0)
        {
            recipient.Header.Add(recipient.emptyViewModel);
            recipient.Current = recipient.Header[0];
        }
    }

    [RelayCommand]
    private void Initialize()
    {
        Header.Add(emptyViewModel);
        Current = Header[0];

        Navigate(Current);
    }

    [RelayCommand]
    private void Navigate(ItemViewModel? selection)
    {
        Current = selection;
        navigationService.Navigate(Current);
    }
}