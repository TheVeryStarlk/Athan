using System;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Shell.Items;
using Athan.UI.Features.Tasbih;
using Athan.UI.Features.Welcome;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Athan.UI.Features.Shell;

internal sealed class NavigationService : INavigationService
{
    public Frame? Frame { get; set; }

    public void Navigate(ItemViewModel? viewModel)
    {
        var type = viewModel switch
        {
            WelcomeViewModel => typeof(WelcomeView),
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