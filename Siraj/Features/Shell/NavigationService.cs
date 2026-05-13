using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Siraj.Features.Prayers;
using Siraj.Features.Settings;
using Siraj.Features.Shell.Items;
using Siraj.Features.Tasbih;
using Siraj.Features.Welcome;

namespace Siraj.Features.Shell;

internal sealed class NavigationService : INavigationService
{
    public Frame? Frame { get; set; }

    public void Navigate(ItemViewModel? viewModel)
    {
        var type = viewModel switch
        {
            PrayersViewModel => typeof(PrayersView),
            SettingsViewModel => typeof(SettingsView),
            TasbihViewModel => typeof(TasbihView),
            WelcomeViewModel => typeof(WelcomeView),
            _ => throw new ArgumentOutOfRangeException()
        };

        Frame?.Navigate(type, viewModel, new EntranceNavigationTransitionInfo());
    }
}

internal interface INavigationService
{
    public void Navigate(ItemViewModel? viewModel);
}