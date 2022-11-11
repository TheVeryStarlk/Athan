using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Services;
using Avalonia.Themes.Fluent;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private INavigable? navigable;

    private readonly NavigationService navigationService;
    private readonly SettingService settingService;
    private readonly ThemeService themeService;

    public ShellViewModel(NavigationService navigationService, SettingService settingService,
        ThemeService themeService)
    {
        this.navigationService = navigationService;
        this.settingService = settingService;
        this.themeService = themeService;

        navigationService.Navigated += viewModel => Navigable = viewModel;
        NetworkChange.NetworkAvailabilityChanged += NetworkChangeOnNetworkAvailabilityChanged;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var settings = await settingService.ReadAsync();
        themeService.Update(settings?.Theme ?? FluentThemeMode.Light);

        if (settings?.Location is null)
        {
            navigationService.NavigateTo(ViewModelLocator.LocationViewModel);
        }
        else
        {
            await navigationService.NavigateToAsync(ViewModelLocator.DashboardViewModel, settings);
        }
    }

    [RelayCommand]
    private async Task ClosingAsync()
    {
        await settingService.SaveAsync();
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