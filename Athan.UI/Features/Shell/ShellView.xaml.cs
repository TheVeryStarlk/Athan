using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellView : Page
{
    public ShellViewModel ViewModel { get; }

    public ShellView(ShellViewModel viewModel, ViewModelConverter viewModelConverter)
    {
        ViewModel = viewModel;
        InitializeComponent();

        Shell.Content = viewModelConverter.Convert(ViewModel.Current!);

        WeakReferenceMessenger.Default.Register<ReadyMessage>(
            this,
            (_, _) => Shell.Content = viewModelConverter.Convert(ViewModel.Current!));
    }
}