using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayersView : Page
{
    public PrayersViewModel ViewModel
    {
        get
        {
            ArgumentNullException.ThrowIfNull(field);
            return field;
        }
        set;
    }

    public PrayersView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        ViewModel = (PrayersViewModel) eventArgs.Parameter;
        ViewModel.ActivateCommand.Execute(null);

        base.OnNavigatedTo(eventArgs);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs eventArgs)
    {
        ViewModel.DeactivateCommand.Execute(null);
        base.OnNavigatedFrom(eventArgs);
    }
}
