using System;
using System.Linq;
using Windows.Foundation;
using Windows.Graphics;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using WinUIEx;
using Microsoft.Extensions.DependencyInjection;
using Siraj.Features.Locations;
using Siraj.Features.Shell.Items;

namespace Siraj.Features.Shell;

internal sealed partial class ShellView : WindowEx
{
    private readonly ShellViewModel viewModel;
    private readonly DispatcherQueueTimer searchBounceTimer;

    public ShellView(ShellViewModel viewModel)
    {
        this.viewModel = viewModel;

        InitializeComponent();
        SetTitleBar(TitleBar);

        this.CenterOnScreen();

        Closed += OnClosed;

        ExtendsContentIntoTitleBar = true;

        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

        searchBounceTimer = DispatcherQueue.CreateTimer();
        searchBounceTimer.IsRepeating = false;
        searchBounceTimer.Interval = TimeSpan.FromMilliseconds(300);
        searchBounceTimer.Tick += SearchBounceTimerTick;
    }

    protected override void OnStateChanged(WindowState state)
    {
        NavigationView.Margin = WindowState is WindowState.Maximized ? new Thickness(0, -1, 0, 0) : new Thickness(0, -2, 0, 0);
    }

    private void OnClosed(object sender, WindowEventArgs eventArgs)
    {
        searchBounceTimer.Stop();
        searchBounceTimer.Tick -= SearchBounceTimerTick;
        viewModel.SaveCommand.Execute(null);
    }
    
    // https://discord.com/channels/372137812037730304/671870147354427422/1492298194749620236
    private void TitleBarSizeChanged(object sender, SizeChangedEventArgs eventArgs)
    {
        var scale = TitleBar.XamlRoot.RasterizationScale;

        var elements = new FrameworkElement[]
        {
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

    private void ToggleButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        NavigationView.IsPaneOpen = !NavigationView.IsPaneOpen;
    }

    private void SearchInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventArgs)
    {
        SearchBox.Focus(FocusState.Programmatic);
    }

    private void SearchBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs eventArgs)
    {
        if (eventArgs.Reason is not AutoSuggestionBoxTextChangeReason.UserInput)
        {
            return;
        }

        searchBounceTimer.Stop();
        searchBounceTimer.Start();
    }

    private async void SearchBounceTimerTick(DispatcherQueueTimer sender, object args)
    {
        sender.Stop();

        await viewModel.SearchAsync(SearchBox.Text);

        SearchBox.IsSuggestionListOpen = viewModel.SearchSuggestions.Count is not 0;
    }

    private void SearchSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs eventArgs)
    {
        if (eventArgs.SelectedItem is not Location location)
        {
            return;
        }

        searchBounceTimer.Stop();
        viewModel.SelectSuggestion(location);
        sender.IsSuggestionListOpen = false;
    }

    private void NavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs eventArgs)
    {
        viewModel.NavigateCommand.Execute((ItemViewModel?) eventArgs.SelectedItemContainer?.DataContext);
    }

    private void FrameLoaded(object sender, RoutedEventArgs eventArgs)
    {
        if (Bootstrapper.Services.GetRequiredService<INavigationService>() is NavigationService navigationService)
        {
            navigationService.Frame = Frame;
        }

        viewModel.InitializeCommand.Execute(null);
    }
}
