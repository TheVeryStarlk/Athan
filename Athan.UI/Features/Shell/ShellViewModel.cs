using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged? Current { get; set; }

    public ShellViewModel()
    {
        WeakReferenceMessenger.Default.Register<ShellViewModel, NavigationRequest>(
            this,
            static (self, request) => self.Current = request.ViewModel);
    }
}