using System.ComponentModel;
using Athan.Avalonia.Features.Errors;
using Athan.Avalonia.Features.Prayers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellViewModel : ObservableRecipient, IRecipient<Error>
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; }

    private readonly ErrorViewModel errorViewModel;

    public ShellViewModel(PrayerViewModel prayerViewModel, ErrorViewModel errorViewModel)
    {
        this.errorViewModel = errorViewModel;

        Current = prayerViewModel;

        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(Error error)
    {
        Current = errorViewModel;
    }
}