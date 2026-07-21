using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Siraj.Features.Prayers.Calculation;
using Serilog;
using System;
using Windows.Foundation;
using Windows.UI;

namespace Siraj.Features.Prayers;

internal sealed partial class PrayersView : Page
{
    public PrayersViewModel ViewModel
    {
        get
        {
            ArgumentNullException.ThrowIfNull(field);
            return field;
        }
        set;
    }

    public PrayersView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        ViewModel = (PrayersViewModel) eventArgs.Parameter;

        Log.Debug("Opened prayers view for {LocationName}", ViewModel.Location.Name);

        ViewModel.InitializeCommand.Execute(null);

        base.OnNavigatedTo(eventArgs);
    }

    // I don't like this.
    private void GradientRectangleLoaded(object sender, RoutedEventArgs eventArgs)
    {
        if (!ViewModel.Upcoming.HasValue)
        {
            return;
        }

        var (red, blue, green) = ViewModel.Upcoming.Value switch
        {
            PrayerKind.Fajr => (255, 239, 120),
            PrayerKind.Dhuhr => (255, 208, 120),
            PrayerKind.Asr => (245, 210, 144),
            PrayerKind.Maghrib => (102, 115, 255),
            PrayerKind.Isha => (83, 57, 250),
            _ => throw new ArgumentOutOfRangeException()
        };

        GradientRectangle.Fill = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(0, 1),
            GradientStops =
            {
                new GradientStop
                {
                    Offset = 0,
                    Color = Color.FromArgb(50, (byte) red, (byte) blue, (byte) green)
                },
                new GradientStop
                {
                    Offset = 1,
                    Color = Color.FromArgb(0, (byte) red, (byte) blue, (byte) green)
                }
            }
        };

        var animation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(2.5),
            EasingFunction = new QuinticEase
            {
                EasingMode = EasingMode.EaseOut
            }
        };

        Storyboard.SetTarget(animation, GradientRectangle);
        Storyboard.SetTargetProperty(animation, nameof(GradientRectangle.Opacity));

        var storyboard = new Storyboard();

        storyboard.Children.Add(animation);
        storyboard.Begin();
    }
}