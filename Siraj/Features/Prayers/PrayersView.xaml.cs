using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Siraj.Features.Prayers;

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
        ViewModel.InitializeCommand.Execute(null);

        base.OnNavigatedTo(eventArgs);
    }
}