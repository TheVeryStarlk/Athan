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
    private readonly SettingService settingService;
    private readonly NavigationService navigationService;

    public SettingsViewModel(ThemeService themeService, SettingService settingService,
        NavigationService navigationService)
    {
        this.themeService = themeService;
        this.settingService = settingService;
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
        await settingService.UpdateAsync(new Setting(loadedSetting?.Location!, themeService.Theme));
        await navigationService.NavigateBackwardAsync();
    }
}