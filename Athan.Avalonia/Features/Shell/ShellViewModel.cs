using System.ComponentModel;
using Athan.Avalonia.Features.Prayers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellViewModel(PrayerViewModel prayerViewModel) : ObservableRecipient
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; } = prayerViewModel;
}