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
        var setting = await settingService.ReadAsync();

        themeService.Update(setting?.Theme ?? FluentThemeMode.Light);

        navigationService.NavigateForward(
            setting?.Validate() is true
                ? ViewModelLocator.DashboardViewModel
                : ViewModelLocator.LocationViewModel);
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
            navigationService.NavigateForward(ViewModelLocator.OfflineViewModel);
        }
    }
}