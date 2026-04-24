using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI.Features.Search;

internal sealed partial class SearchView : UserControl
{
    private readonly SearchViewModel viewModel = Bootstrapper.Services.GetRequiredService<SearchViewModel>();

    public SearchView()
    {
        InitializeComponent();
    }
}