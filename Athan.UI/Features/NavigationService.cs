using Athan.UI.Features.Empty;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Shell;
using Athan.UI.Features.Tasbih;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace Athan.UI.Features;

internal sealed class NavigationService : INavigationService
{
    public Frame? Frame { get; set; }

    public void Navigate(ItemViewModel? viewModel)
    {
        var type = viewModel switch
        {
            EmptyViewModel => typeof(EmptyView),
            PrayersViewModel => typeof(PrayersView),
            TasbihViewModel => typeof(TasbihView),
            SettingsViewModel => typeof(SettingsView),
            _ => throw new ArgumentOutOfRangeException()
        };

        Frame?.Navigate(type, viewModel, new EntranceNavigationTransitionInfo());
    }
}

internal interface INavigationService
{
    public void Navigate(ItemViewModel? viewModel);
}