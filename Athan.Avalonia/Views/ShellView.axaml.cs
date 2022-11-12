using System;
using Athan.Avalonia.Messages;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.Views;

internal sealed partial class ShellView : Window
{
    private WindowState oldState;

    public ShellView()
    {
        DataContext = ViewModelLocator.ShellViewModel;
        InitializeComponent();

        ContainerContentControl.PropertyChanged += async (_, args) =>
        {
            if (args.Property == ContentProperty)
            {
                var animation = new Animation()
                {
                    Duration = TimeSpan.FromMilliseconds(750),
                    Easing = new ExponentialEaseOut(),
                    Children =
                    {
                        new KeyFrame()
                        {
                            KeyTime = TimeSpan.FromMilliseconds(0),
                            Setters =
                            {
                                new Setter(OpacityProperty, 0.5),
                                new Setter(ScaleTransform.ScaleXProperty, 0.98),
                                new Setter(ScaleTransform.ScaleYProperty, 0.98),
                            }
                        },
                        new KeyFrame()
                        {
                            KeyTime = TimeSpan.FromMilliseconds(750),
                            Setters =
                            {
                                new Setter(OpacityProperty, 1),
                                new Setter(ScaleTransform.ScaleXProperty, 1.0),
                                new Setter(ScaleTransform.ScaleYProperty, 1.0),
                            }
                        }
                    }
                };

                await animation.RunAsync(ContainerBorder, null);
            }
        };
    }

    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register<TrayIconOpenedMessage>(this, TrayIconOpenedMessageHandler);
        Margin = OffScreenMargin;
    }

    private void TrayIconOpenedMessageHandler(object recipient, TrayIconOpenedMessage message)
    {
        WindowState = WindowState is WindowState.Minimized
            ? oldState
            : WindowState;

        Activate();
    }

    protected override void HandleWindowStateChanged(WindowState state)
    {
        if (state is not WindowState.Minimized)
        {
            oldState = state;
        }

        base.HandleWindowStateChanged(state);
    }
}