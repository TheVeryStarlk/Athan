using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace Athan.UI.Features.Empty;

internal sealed partial class EmptyView : Page
{
    public EmptyViewModel ViewModel
    {
        get
        {
            ArgumentNullException.ThrowIfNull(field);
            return field;
        }
        set;
    }

    public EmptyView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        ViewModel = (EmptyViewModel) eventArgs.Parameter;

        base.OnNavigatedTo(eventArgs);
    }
}