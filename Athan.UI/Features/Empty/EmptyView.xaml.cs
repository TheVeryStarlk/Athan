using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Empty;

internal sealed partial class EmptyView : Page
{
    private readonly EmptyViewModel viewModel = Bootstrapper.Services.GetRequiredService<EmptyViewModel>();
    
    public EmptyView()
    {
        InitializeComponent();
    }
}