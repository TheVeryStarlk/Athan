using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;
using Windows.Foundation;
using Windows.Graphics;
using WinUIEx;

namespace Athan.UI;

internal sealed partial class ShellView : WindowEx
{
    private readonly ShellViewModel viewModel;

    public ShellView(ShellViewModel viewModel)
    {
        this.viewModel = viewModel;

        InitializeComponent();
        SetTitleBar(TitleBar);

        this.CenterOnScreen();

        ExtendsContentIntoTitleBar = true;

        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
    }

    protected override void OnStateChanged(WindowState state)
    {
        NavigationView.Margin = WindowState is WindowState.Maximized ? new Thickness(0, -1, 0, 0) : new Thickness(0, -2, 0, 0);
    }

    // https://discord.com/channels/372137812037730304/671870147354427422/1492298194749620236
    private void TitleBarSizeChanged(object sender, SizeChangedEventArgs eventArgs)
    {
        var scale = TitleBar.XamlRoot.RasterizationScale;

        var elements = new FrameworkElement[]
        {
            BackButton,
            ToggleButton,
            SearchBox
        };

        var rects = elements
            .Select(element =>
            {
                var transform = element.TransformToVisual(null);
                var bounds = transform.TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));

                return new RectInt32(
                    (int) Math.Round(bounds.X * scale),
                    (int) Math.Round(bounds.Y * scale),
                    (int) Math.Round(bounds.Width * scale),
                    (int) Math.Round(bounds.Height * scale)
                );
            })
            .ToArray();

        InputNonClientPointerSource
            .GetForWindowId(AppWindow.Id)
            .SetRegionRects(NonClientRegionKind.Passthrough, rects);
    }

    private void SearchInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventArgs)
    {
        SearchBox.Focus(FocusState.Programmatic);
    }

    private void BackButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Frame.GoBack();
    }

    private void ToggleButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
    }

    private void NavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs eventArgs)
    {
        Type type;

        if (eventArgs.IsSettingsSelected)
        {
            type = typeof(SettingsView);
        }
        else if (NavigationView.SelectedItem is PrayersViewModel)
        {
            type = typeof(PrayersView);
        }
        else
        {
            type = typeof(TasbihCountingView);
        }

        Frame.Navigate(type, NavigationView.SelectedItem, eventArgs.RecommendedNavigationTransitionInfo);
    }

    private void FrameLoaded(object sender, RoutedEventArgs eventArgs)
    {
        if (App.Services.GetRequiredService<INavigationService>() is NavigationService service)
        {
            service.Frame = Frame;
        }

        NavigationView.SelectedItem = viewModel.Items[0];
        Frame.Navigate(typeof(PrayersView), NavigationView.SelectedItem, new EntranceNavigationTransitionInfo());
    }

    private void FrameNavigated(object sender, NavigationEventArgs eventArgs)
    {
        BackButton.Visibility = Frame.BackStackDepth > 1 ? Visibility.Visible : Visibility.Collapsed;

        if (Frame.SourcePageType == typeof(SettingsView))
        {
            NavigationView.SelectedItem = NavigationView.SettingsItem;
        }
        else
        {
            NavigationView.SelectedItem = viewModel.Items.First(item => item == eventArgs.Parameter);
        }
    }
}

internal interface IItem;

internal sealed class Separator : IItem;

internal sealed partial class NavigationViewItemTemplateSelector : DataTemplateSelector
{
    public DataTemplate? PrayersTemplate { get; set; }

    public DataTemplate? TasbihCountingTemplate { get; set; }

    public DataTemplate? SeparatorTemplate { get; set; }

    protected override DataTemplate SelectTemplateCore(object item)
    {
        ArgumentNullException.ThrowIfNull(PrayersTemplate);
        ArgumentNullException.ThrowIfNull(SeparatorTemplate);
        ArgumentNullException.ThrowIfNull(TasbihCountingTemplate);

        if (item is PrayersViewModel)
        {
            return PrayersTemplate;
        }
        else if (item is TasbihCountingViewModel)
        {
            return TasbihCountingTemplate;
        }
        else
        {
            return SeparatorTemplate;
        }
    }
}