using System.ComponentModel;
using Athan.UI.Features.Welcome;
using Microsoft.UI.Xaml;

namespace Athan.UI.Features.Shell;

internal sealed class ViewConverter(Func<WelcomeViewModel, WelcomeView> welcomeViewFactory)
{
    public FrameworkElement Convert(INotifyPropertyChanged value)
    {
        return value switch
        {
            WelcomeViewModel viewModel => welcomeViewFactory(viewModel),
            _ => throw new ArgumentException("Unknown type.")
        };
    }
}