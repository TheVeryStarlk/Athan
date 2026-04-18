using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI;

internal sealed partial class TasbihCountingView : Page
{
    private readonly TasbihCountingViewModel viewModel = App.Services.GetRequiredService<TasbihCountingViewModel>();

    public TasbihCountingView()
    {
        InitializeComponent();
    }
}