using System.Windows;
using System.Windows.Controls;

namespace Athan.Desktop.Features;

public sealed class SpacingSetter
{
    public static readonly DependencyProperty SpacingProperty =
        DependencyProperty.RegisterAttached(
            "Spacing",
            typeof(float),
            typeof(SpacingSetter),
            new UIPropertyMetadata(0F, MarginChanged));

    public static float GetSpacing(DependencyObject dependencyObject)
    {
        return (float) dependencyObject.GetValue(SpacingProperty);
    }

    public static void SetSpacing(DependencyObject dependencyObject, float value)
    {
        dependencyObject.SetValue(SpacingProperty, value);
    }

    private static void MarginChanged(object sender, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (sender is Panel panel)
        {
            panel.Loaded += PanelLoaded;
        }
    }

    private static void PanelLoaded(object sender, RoutedEventArgs eventArgs)
    {
        var panel = (Panel) sender;

        if (panel.Children.Count < 1)
        {
            return;
        }

        var thickness = panel is StackPanel { Orientation: Orientation.Horizontal }
            ? new Thickness(GetSpacing(panel), 0, 0, 0)
            : new Thickness(0, GetSpacing(panel), 0, 0);

        var first = true;

        foreach (var child in panel.Children)
        {
            if (child is not FrameworkElement frameworkElement)
            {
                continue;
            }

            if (!first)
            {
                frameworkElement.Margin = thickness;
            }
            else
            {
                first = false;
            }
        }
    }
}