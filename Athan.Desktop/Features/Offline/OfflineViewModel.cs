using Athan.Desktop.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Offline;

public sealed partial class OfflineViewModel(NavigationService navigationService) : ObservableObject
{
    [RelayCommand]
    private void TryAgain()
    {
        navigationService.NavigateBackward();
    }
}