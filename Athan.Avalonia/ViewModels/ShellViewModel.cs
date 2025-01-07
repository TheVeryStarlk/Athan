using System.ComponentModel;
using Athan.Avalonia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableRecipient, IRecipient<Error>
{
    private readonly ErrorViewModel errorViewModel;

    public ShellViewModel(PrayerViewModel prayerViewModel, ErrorViewModel errorViewModel)
    {
        this.errorViewModel = errorViewModel;

        Current = prayerViewModel;

        WeakReferenceMessenger.Default.Register(this);
    }

    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; }

    public void Receive(Error error)
    {
        Current = errorViewModel;
    }
}