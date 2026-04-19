using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Athan.UI;

internal sealed partial class ShellViewModel : ObservableObject
{
    public ObservableCollection<IItem> Items { get; } =
    [
        new PrayersViewModel
        {
            Emoji = "\uECAF",
            Title = "My location",
            Family = "Segoe Fluent Icons",
        },
        new TasbihCountingViewModel(),
        new Separator(),
        new PrayersViewModel
        {
            Emoji = "🌃",
            Title = "Kuwait, Kuwait",
            Family = "Segoe MDL2 Emoji"
        },
        new PrayersViewModel
        {
            Emoji = "🌄",
            Title = "Amman, Jordan",
            Family = "Segoe MDL2 Emoji"
        },
        new PrayersViewModel
        {
            Emoji = "🌇",
            Title = "Paris, France",
            Family = "Segoe MDL2 Emoji"
        }
    ];
}