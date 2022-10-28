using System.Threading.Tasks;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayersViewModel : ObservableObject
{
    public Location? Location { get; set; }
    
    [ObservableProperty]
    private string? closestPrayer;
    
    private readonly PrayerService prayerService;

    public PrayersViewModel(PrayerService prayerService)
    {
        this.prayerService = prayerService;
        WeakReferenceMessenger.Default.Register<ActivatedMessage>(this, ActivatedMessageHandler);
    }

    [RelayCommand]
    private async Task InitializedAsync()
    {
        if (Location is null)
        {
            return;
        }
        
        var prayers = await prayerService.GetTimingsAsync(Location.City, Location.Country);
        ClosestPrayer = prayerService.GetClosest(prayers).Name;
    }
    
    private async void ActivatedMessageHandler(object recipient, ActivatedMessage message)
    {
        await InitializedAsync();
    }
}