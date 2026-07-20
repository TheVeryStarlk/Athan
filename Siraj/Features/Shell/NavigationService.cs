using System;
using Microsoft.UI.Xaml.Controls;
using Siraj.Features.Prayers;
using Siraj.Features.Settings;
using Siraj.Features.Shell.Items;
using Siraj.Features.Tasbih;
using Siraj.Features.Welcome;
using Serilog;

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

        Log.Debug("Navigating to {ViewModelType}", viewModel.GetType().Name);
        Frame?.Navigate(type, viewModel);
    }
}

internal interface INavigationService
{
    public void Navigate(ItemViewModel? viewModel);
}