using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Shell.Items;

internal abstract partial class ItemViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial string? Glyph { get; set; }

    public bool Deletable { get; init; }
}

internal abstract partial class HeaderViewModel : ItemViewModel
{
    [RelayCommand]
    private void Delete()
    {
        WeakReferenceMessenger.Default.Send(new DeleteMessage(this));
    }
}

internal abstract class FooterViewModel : ItemViewModel;