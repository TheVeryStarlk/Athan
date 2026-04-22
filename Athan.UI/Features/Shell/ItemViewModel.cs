using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    public Action<HeaderViewModel>? OnDelete { get; set; }

    [RelayCommand]
    private void Delete() => OnDelete?.Invoke(this);
}

internal abstract class FooterViewModel : ItemViewModel;