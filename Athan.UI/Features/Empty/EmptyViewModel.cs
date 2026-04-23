using System;
using System.Threading.Tasks;
using Athan.UI.Features.Shell.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Empty;

internal sealed partial class EmptyViewModel(DialogService dialogService) : ObservableObject
{
    [RelayCommand]
    private async Task Foo()
    {
        WeakReferenceMessenger.Default.Send(new AddMessage());
        WeakReferenceMessenger.Default.Send(new AddMessage());
        WeakReferenceMessenger.Default.Send(new AddMessage());

        return;
        
        await dialogService.ShowMessageAsync(
            "Where are you?", 
            $"Please enable location access for Athan{Environment.NewLine}"
            + "Or search manually for a location.");
    }
}
