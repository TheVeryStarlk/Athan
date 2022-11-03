using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class SettingsViewModel : ObservableObject, INavigable
{
    public string Title => "Settings";

    private readonly ThemeService themeService;
    private readonly NavigationService navigationService;

    public SettingsViewModel(ThemeService themeService, NavigationService navigationService)
    {
        this.themeService = themeService;
        this.navigationService = navigationService;
    }

    public Task Navigated(Setting setting)
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        themeService.Toggle();
    }

    [RelayCommand]
    private void Relocate()
    {
        navigationService.GoForward(ViewModelLocator.LocationViewModel);
    }

    [RelayCommand]
    private void NavigateBackward()
    {
        navigationService.GoBackward();
    }
}