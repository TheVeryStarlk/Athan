using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI;

internal sealed partial class PrayersView : Page
{
    private readonly PrayersViewModel viewModel = App.Services.GetRequiredService<PrayersViewModel>();

    public PrayersView()
    {
        InitializeComponent();
    }
}