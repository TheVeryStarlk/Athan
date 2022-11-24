using Athan.Avalonia.Contracts;
using Athan.Avalonia.Languages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using Avalonia.Themes.Fluent;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class SettingsViewModel : ObservableObject, INavigable
{
    public string Title => Language.Settings;

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

    public int SelectedLanguageIndex
    {
        set
        {
            SetProperty(ref selectedLanguageIndex, value);
            languageService.Update((ApplicationLanguage) value);
        }
        get => selectedLanguageIndex;
    }

    private int selectedLanguageIndex;

    private Setting? loadedSetting;

    private readonly ThemeService themeService;
    private readonly SettingService settingService;
    private readonly LanguageService languageService;
    private readonly NavigationService navigationService;

    public SettingsViewModel(ThemeService themeService, SettingService settingService,
        LanguageService languageService, NavigationService navigationService)
    {
        this.themeService = themeService;
        this.settingService = settingService;
        this.languageService = languageService;
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        SelectedThemeIndex = (int) themeService.Theme;
        loadedSetting = await settingService.ReadAsync();
        SelectedLanguageIndex = (int) languageService.Read();
    }

    [RelayCommand]
    private void RelocateAsync()
    {
        navigationService.NavigateForward(ViewModelLocator.LocationViewModel);
    }

    [RelayCommand]
    private void SaveAsync()
    {
        settingService.Update(new Setting(loadedSetting?.Location!, themeService.Theme,
            (ApplicationLanguage) SelectedLanguageIndex));

        navigationService.NavigateBackward();
    }
}