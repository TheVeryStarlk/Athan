using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI;

internal sealed partial class TasbihView : Page
{
    private readonly TasbihViewModel viewModel = App.Services.GetRequiredService<TasbihViewModel>();

    public TasbihView()
    {
        InitializeComponent();
    }
}