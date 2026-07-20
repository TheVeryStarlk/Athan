using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Serilog;

namespace Siraj.Features.Settings;

internal sealed partial class SettingsView : Page
{
    public SettingsViewModel ViewModel
    {
        get
        {
            ArgumentNullException.ThrowIfNull(field);
            return field;
        }
        set;
    }

    public SettingsView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        Log.Debug("Opened settings view");

        ViewModel = (SettingsViewModel) eventArgs.Parameter;
        ViewModel.InitializeCommand.Execute(null);

        base.OnNavigatedTo(eventArgs);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs eventArgs)
    {
        Log.Debug("Leaving settings view and saving settings");

        ViewModel.SaveCommand.Execute(null);

        base.OnNavigatedFrom(eventArgs);
    }
}