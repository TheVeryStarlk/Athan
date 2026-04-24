using System;
using System.Globalization;
using Athan.UI.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayersViewModel : HeaderViewModel
{
    [ObservableProperty]
    public partial string? Hijri { get; set; }

    [RelayCommand]
    private void Initialize()
    {
        Hijri = DateTimeOffset.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, new CultureInfo("ar-SA"));
    }
}