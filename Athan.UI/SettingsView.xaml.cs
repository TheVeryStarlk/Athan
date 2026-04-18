using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI;

internal sealed partial class SettingsView : Page
{
    private readonly SettingsViewModel viewModel = App.Services.GetRequiredService<SettingsViewModel>();

    public SettingsView()
    {
        InitializeComponent();
    }
}