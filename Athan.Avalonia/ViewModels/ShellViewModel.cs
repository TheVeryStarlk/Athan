using System;
using System.ComponentModel;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Services;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; }

    private readonly WelcomeViewModel welcomeViewModel;
    private readonly PrayerViewModel prayerViewModel;

    public ShellViewModel(StorageService storageService, WelcomeViewModel welcomeViewModel, PrayerViewModel prayerViewModel)
    {
        if (storageService.TryGet<Location>(nameof(Location), out _))
        {
            Current = prayerViewModel;
            WeakReferenceMessenger.Default.Send(new NavigationRequest(nameof(PrayerViewModel)));
        }
        else
        {
            Current = welcomeViewModel;
        }

        this.welcomeViewModel = welcomeViewModel;
        this.prayerViewModel = prayerViewModel;

        WeakReferenceMessenger.Default.Register<ShellViewModel, NavigationRequest>(
            this,
            static (self, message) => self.Current = message.Name switch
            {
                nameof(WelcomeViewModel) => self.welcomeViewModel,
                nameof(PrayerViewModel) => self.prayerViewModel,
                _ => throw new ArgumentException()
            });
    }
}