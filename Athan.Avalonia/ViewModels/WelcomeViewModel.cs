using System.Threading.Tasks;
using Athan.Avalonia.Extensions;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Services;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class WelcomeViewModel(LocationService locationService, StorageService storageService) : ObservableObject
{
    [RelayCommand]
    private async Task StartAsync()
    {
        var result = await locationService
            .GetAsync()
            .ThenAsync(location => storageService.Set(nameof(Location), location));

        if (result.IsFailure())
        {
            return;
        }

        WeakReferenceMessenger.Default.Send(new NavigationRequest(nameof(PrayerViewModel)));
    }
}