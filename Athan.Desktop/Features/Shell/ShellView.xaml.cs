using CommunityToolkit.Mvvm.Messaging;
using Wpf.Ui.Appearance;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellView
{
    private readonly ShellViewModel viewModel;
    private readonly UserControlFactory userControlFactory;

    public ShellView(ShellViewModel viewModel, UserControlFactory userControlFactory)
    {
        this.viewModel = viewModel;
        this.userControlFactory = userControlFactory;

        DataContext = viewModel;
        InitializeComponent();

        ApplicationThemeManager.Apply(this);

        // Force the shell to show the current view.
        // We do this before registering the message to not update twice.
        Update();

        WeakReferenceMessenger.Default.Register<ShellView, NavigationRequest>(
            this,
            static (self, _) => self.Update());
    }

    private void Update()
    {
        Shell.Content = userControlFactory.Create(viewModel.Current!);

        var name = viewModel.Current!.GetType().Name;
        Title = $"Athan • {name[..name.IndexOf("ViewModel", StringComparison.Ordinal)]}";
    }
}