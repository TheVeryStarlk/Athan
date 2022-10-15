using Athan.Avalonia.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed class DashboardViewModel : ObservableObject, INavigable
{
    public string Title => "Dashboard";
}