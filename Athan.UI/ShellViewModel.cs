using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Athan.UI;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<INotifyPropertyChanged> Items { get; } =
    [
        new LocationViewModel(),
        new TasbihViewModel(),
        new PrayersViewModel
        {
            Icon = "🌃",
            Title = "Kuwait, Kuwait"
        },
        new PrayersViewModel
        {
            Icon = "🌄",
            Title = "Amman, Jordan"
        },
        new PrayersViewModel
        {
            Icon = "🌇",
            Title = "Paris, France"
        }
    ];
}