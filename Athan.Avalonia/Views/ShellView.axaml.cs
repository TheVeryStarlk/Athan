using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia.Views;

internal sealed partial class ShellView : Window
{
    private readonly ShellViewModel viewModel = App.Current.Services.GetRequiredService<ShellViewModel>();

    public ShellView()
    {
        DataContext = viewModel;
        InitializeComponent();

        Activated += (_, _) => viewModel.CheckForInternetConnectionCommand.Execute(null);
    }
}