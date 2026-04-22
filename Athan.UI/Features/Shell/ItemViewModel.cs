using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.UI.Features.Shell;

internal abstract partial class ItemViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial string? Glyph { get; set; }
}

internal abstract partial class HeaderViewModel : ItemViewModel
{
    public ICommand? DeleteCommand { get; set; }
}

internal abstract class FooterViewModel : ItemViewModel;