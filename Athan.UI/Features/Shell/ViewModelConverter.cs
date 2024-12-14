using System.ComponentModel;
using Athan.UI.Features.Prayers;
using Athan.UI.Features.Welcome;
using Microsoft.UI.Xaml;

namespace Athan.UI.Features.Shell;

internal sealed class ViewModelConverter(
    Func<WelcomeViewModel, WelcomeView> welcomeViewFactory,
    Func<PrayerViewModel, PrayerView> prayerViewFactory)
{
    public FrameworkElement Convert(INotifyPropertyChanged value)
    {
        return value switch
        {
            WelcomeViewModel viewModel => welcomeViewFactory(viewModel),
            PrayerViewModel viewModel => prayerViewFactory(viewModel),
            _ => throw new ArgumentException("Unknown type.")
        };
    }
}