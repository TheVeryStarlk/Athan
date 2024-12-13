using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Athan.UI;

internal sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SystemBackdrop = new MicaBackdrop();
    }
}