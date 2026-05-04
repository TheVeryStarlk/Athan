using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Tasbih;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using Athan.UI.Features.Shell.Items;
using Athan.UI.Features.Welcome;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<HeaderViewModel> Header { get; } = [];

    public ObservableCollection<FooterViewModel> Footer { get; }

    [ObservableProperty]
    public partial ItemViewModel? Current { get; set; }

    private readonly INavigationService navigationService;
    private readonly PrayersViewModelFactory prayersViewModelFactory;
    private readonly WelcomeViewModel welcomeViewModel;

    public ShellViewModel(
        INavigationService navigationService,
        PrayersViewModelFactory prayersViewModelFactory,
        WelcomeViewModel welcomeViewModel,
        TasbihViewModel tasbihViewModel,
        SettingsViewModel settingsViewModel)
    {
        this.navigationService = navigationService;
        this.prayersViewModelFactory = prayersViewModelFactory;
        this.welcomeViewModel = welcomeViewModel;

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
        if (recipient.Header[0] is WelcomeViewModel)
        {
            recipient.Header.RemoveAt(0);
        }

        var instance = recipient.prayersViewModelFactory.Create(message.Location);

        recipient.Header.Add(instance);
        recipient.Navigate(recipient.Header.Last());
    }

    private static void Delete(ShellViewModel recipient, DeleteMessage message)
    {
        recipient.Header.Remove(message.Instance);
        
        if (recipient.Header.Count is 0)
        {
            recipient.Header.Add(recipient.welcomeViewModel);
        }

        recipient.Navigate(recipient.Header[0]);
    }

    [RelayCommand]
    private void Initialize()
    {
        Header.Add(welcomeViewModel);
        Navigate(welcomeViewModel);
    }

    [RelayCommand]
    private void Navigate(ItemViewModel? selection)
    {
        Current = selection ?? Current;
        navigationService.Navigate(Current);
    }
}