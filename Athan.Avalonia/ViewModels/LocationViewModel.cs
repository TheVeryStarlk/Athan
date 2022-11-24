using Athan.Avalonia.Contracts;
using Athan.Avalonia.Languages;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class LocationViewModel : ObservableObject, INavigable
{
    public string Title => Language.WhereAreYou;

    [ObservableProperty]
    private string message = Language.HangTight;

    [ObservableProperty]
    private bool canContinue;

    private readonly LocationService locationService;
    private readonly SettingService settingService;
    private readonly PollService pollService;
    private readonly ThemeService themeService;
    private readonly NavigationService navigationService;

    public LocationViewModel(LocationService locationService, SettingService settingService,
        PollService pollService, ThemeService themeService, NavigationService navigationService)
    {
        this.locationService = locationService;
        this.settingService = settingService;
        this.pollService = pollService;
        this.themeService = themeService;
        this.navigationService = navigationService;

        WeakReferenceMessenger.Default.Register<DialogTryAgainRequestMessage>(this,
            DialogTryAgainRequestMessageHandler);
    }

    private async void DialogTryAgainRequestMessageHandler(object recipient,
        DialogTryAgainRequestMessage dialogTryAgainRequestMessage)
    {
        if (dialogTryAgainRequestMessage.Requester is not LocationViewModel)
        {
            return;
        }

        await InitializeAsync();
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var location = await pollService.HandleAsync(async () => await locationService.GetLocationAsync());

        if (location.IsFailed)
        {
            WeakReferenceMessenger.Default.Send(new DialogRequestMessage(this, location.Errors.ToArray()));
            return;
        }

        var oldSetting = await settingService.ReadAsync();

        settingService.Update(new Setting(location.Value, themeService.Theme,
            oldSetting?.Language ?? ApplicationLanguage.English));

        Message = $"{Language.YouSeemToBeIn} {location.Value}.";
        CanContinue = true;
    }

    [RelayCommand]
    private void NavigateForward()
    {
        navigationService.NavigateForward(ViewModelLocator.DashboardViewModel);
    }
}