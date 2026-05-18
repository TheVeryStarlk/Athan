using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Siraj.Features.Locations;
using Siraj.Features.Prayers;
using Siraj.Features.Settings;
using Siraj.Features.Shell.Items;
using Siraj.Features.Tasbih;
using Siraj.Features.Welcome;

namespace Siraj.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    private const int MaxSearchSuggestions = 5;

    public ObservableCollection<HeaderViewModel> Header { get; } = [];

    public ObservableCollection<FooterViewModel> Footer { get; }

    public ObservableCollection<Location> SearchSuggestions { get; } = [];

    [ObservableProperty]
    public partial ItemViewModel? Current { get; set; }

    private int searchVersion;
    private readonly INavigationService navigationService;
    private readonly LocationService locationService;
    private readonly SettingsService settingsService;
    private readonly PrayersViewModelFactory prayersViewModelFactory;
    private readonly WelcomeViewModel welcomeViewModel;

    public ShellViewModel(
        INavigationService navigationService,
        LocationService locationService,
        SettingsService settingsService,
        PrayersViewModelFactory prayersViewModelFactory,
        TasbihViewModel tasbihViewModel,
        WelcomeViewModel welcomeViewModel,
        SettingsViewModel settingsViewModel)
    {
        this.navigationService = navigationService;
        this.locationService = locationService;
        this.settingsService = settingsService;
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
        var locations = settingsService.Locations;

        if (locations.Length is 0)
        {
            Header.Add(welcomeViewModel);
        }
        else
        {
            foreach (var location in locations)
            {
                Header.Add(prayersViewModelFactory.Create(location));
            }
        }

        Navigate(Header[^1]);
    }

    [RelayCommand]
    private void Navigate(ItemViewModel? selection)
    {
        if (Current == selection || selection is null)
        {
            return;
        }

        Current = selection;
        navigationService.Navigate(Current);
    }

    [RelayCommand]
    private void Save()
    {
        settingsService.Locations = Header.OfType<PrayersViewModel>().Select(header => header.Location).ToArray();
    }

    public async Task SearchAsync(string query)
    {
        var version = Interlocked.Increment(ref searchVersion);
        query = query.Trim();

        if (string.IsNullOrWhiteSpace(query))
        {
            SearchSuggestions.Clear();
            return;
        }

        try
        {
            var locations = await locationService.SearchAsync(query);

            if (version != searchVersion)
            {
                return;
            }

            SearchSuggestions.Clear();

            foreach (var location in locations.Take(MaxSearchSuggestions))
            {
                SearchSuggestions.Add(location);
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Location search failed:");
            Debug.WriteLine(exception);

            if (version != searchVersion)
            {
                return;
            }

            SearchSuggestions.Clear();
        }
    }

    public void SelectSuggestion(Location location)
    {
        Add(this, new AddMessage(location));
        SearchSuggestions.Clear();
    }
}
