using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private INavigable? navigable;

    private readonly NavigationService navigationService;
    private readonly SettingsService settingsService;
    private readonly ThemeService themeService;

    public ShellViewModel(NavigationService navigationService, SettingsService settingsService,
        ThemeService themeService)
    {
        this.navigationService = navigationService;
        this.settingsService = settingsService;
        this.themeService = themeService;

        navigationService.Navigated += viewModel => Navigable = viewModel;
        NetworkChange.NetworkAvailabilityChanged += NetworkChangeOnNetworkAvailabilityChanged;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var settings = await settingsService.ReadAsync();

        if (settings?.Location is null)
        {
            navigationService.NavigateTo(ViewModelLocator.LocationViewModel);
        }
        else
        {
            themeService.Update(settings.Theme);
            await navigationService.NavigateToAsync(ViewModelLocator.DashboardViewModel, settings);
        }
    }

    private void NetworkChangeOnNetworkAvailabilityChanged(object? sender, NetworkAvailabilityEventArgs eventArgs)
    {
        if (eventArgs.IsAvailable)
        {
            navigationService.NavigateBackward();
        }
        else
        {
            navigationService.NavigateTo(ViewModelLocator.OfflineViewModel);
        }
    }
}