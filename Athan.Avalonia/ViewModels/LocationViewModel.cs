using Athan.Avalonia.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed class LocationViewModel : ObservableObject, INavigable
{
    public string Title => "Where do you live";
}