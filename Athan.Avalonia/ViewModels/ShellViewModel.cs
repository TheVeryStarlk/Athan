using System.Net.NetworkInformation;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using Avalonia.Themes.Fluent;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private INavigable? navigable;

    [ObservableProperty]
    private DialogViewModel dialogViewModel;

    private readonly NavigationService navigationService;
    private readonly SettingService settingService;
    private readonly ThemeService themeService;
    private readonly LanguageService languageService;

    public ShellViewModel(NavigationService navigationService, SettingService settingService,
        ThemeService themeService, LanguageService languageService, DialogViewModel dialogViewModel)
    {
        this.navigationService = navigationService;
        this.settingService = settingService;
        this.themeService = themeService;
        this.languageService = languageService;
        this.dialogViewModel = dialogViewModel;

        SetProperty(ref dialogViewModel, dialogViewModel);
        WeakReferenceMessenger.Default.Register<DialogRequestMessage>(this, DialogRequestMessageHandler);

        navigationService.Navigated += viewModel => Navigable = viewModel;
        NetworkChange.NetworkAvailabilityChanged += NetworkChangeOnNetworkAvailabilityChanged;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var setting = await settingService.ReadAsync();

        themeService.Update(setting?.Theme ?? FluentThemeMode.Light);
        languageService.Update(setting?.Language ?? ApplicationLanguage.English);

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

    private void DialogRequestMessageHandler(object recipient, DialogRequestMessage message)
    {
        dialogViewModel.Message = message.Errors.First().Message;
        dialogViewModel.Open(message.Requester);
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