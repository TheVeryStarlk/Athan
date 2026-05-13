using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Siraj.Features.Welcome;

internal sealed partial class WelcomeView : Page
{
    public WelcomeViewModel ViewModel
    {
        get
        {
            ArgumentNullException.ThrowIfNull(field);
            return field;
        }
        set;
    }

    public WelcomeView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        ViewModel = (WelcomeViewModel) eventArgs.Parameter;
        base.OnNavigatedTo(eventArgs);
    }
}