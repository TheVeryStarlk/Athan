using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using Avalonia.Themes.Fluent;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class SettingsViewModel : ObservableObject, INavigable
{
    public string Title => "Settings";

    public int SelectedThemeIndex
    {
        set
        {
            SetProperty(ref selectedThemeIndex, value);
            themeService.Update((FluentThemeMode) value);
        }
        get => selectedThemeIndex;
    }

    private int selectedThemeIndex;

    private Setting? loadedSetting;

    private readonly ThemeService themeService;
    private readonly SettingsService settingsService;
    private readonly NavigationService navigationService;

    public SettingsViewModel(ThemeService themeService, SettingsService settingsService,
        NavigationService navigationService)
    {
        this.themeService = themeService;
        this.settingsService = settingsService;
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private void Initialize()
    {
        SelectedThemeIndex = (int) themeService.Theme;
    }

    public Task Navigated(Setting setting)
    {
        loadedSetting = setting;
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void NavigateToLocation()
    {
        navigationService.NavigateTo(ViewModelLocator.LocationViewModel);
    }

    [RelayCommand]
    private async Task NavigateBackwardAsync()
    {
        await settingsService.UpdateAsync(new Setting(loadedSetting?.Location!, themeService.Theme));
        await navigationService.NavigateBackwardAsync();
    }
}