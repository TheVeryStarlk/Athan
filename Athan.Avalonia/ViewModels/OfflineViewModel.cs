using Athan.Avalonia.Contracts;
using Athan.Avalonia.Languages;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed class OfflineViewModel : ObservableObject, INavigable
{
    public string Title => Language.YouSeemToBeOffline;
}