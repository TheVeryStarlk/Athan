using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features;

internal abstract partial class ItemViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial string? Glyph { get; set; }

    [ObservableProperty]
    public partial bool Deletable { get; set; } = true;
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