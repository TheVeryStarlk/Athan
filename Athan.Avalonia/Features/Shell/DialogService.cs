using System.ComponentModel;
using System.Threading.Tasks;
using DialogHostAvalonia;

namespace Athan.Avalonia.Features.Shell;

internal sealed class DialogService
{
    public Task ShowAsync(INotifyPropertyChanged viewModel)
    {
        return DialogHost.Show(viewModel);
    }

    public void Close()
    {
        DialogHost.Close(null);
    }
}