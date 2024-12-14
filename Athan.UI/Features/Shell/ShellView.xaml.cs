using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellView : Page
{
    public ShellViewModel ViewModel { get; }

    private readonly ViewModelConverter viewModelConverter;

    public ShellView(ShellViewModel viewModel, ViewModelConverter viewModelConverter)
    {
        this.viewModelConverter = viewModelConverter;
        ViewModel = viewModel;

        InitializeComponent();

        WeakReferenceMessenger.Default.Register<ShellView, NavigationRequest>(
            this,
            static (self, request) => self.Shell.Content = self.viewModelConverter.Convert(request.ViewModel));
    }
}