using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;

namespace Athan.Avalonia.Views;

internal sealed partial class DialogView : UserControl
{
    public DialogView()
    {
        InitializeComponent();

        PropertyChanged += async (_, eventArgs) =>
        {
            if (eventArgs.Property == IsVisibleProperty)
            {
                var animation = new Animation()
                {
                    Duration = TimeSpan.FromSeconds(1),
                    Easing = new ExponentialEaseOut(),
                    Children =
                    {
                        new KeyFrame()
                        {
                            KeyTime = TimeSpan.FromSeconds(0),
                            Setters =
                            {
                                new Setter(OpacityProperty, 0.0),
                                new Setter(TranslateTransform.YProperty, 5.0D),
                            }
                        },
                        new KeyFrame()
                        {
                            KeyTime = TimeSpan.FromSeconds(1),
                            Setters =
                            {
                                new Setter(OpacityProperty, 1.0),
                                new Setter(TranslateTransform.YProperty, 0.0D),
                            }
                        }
                    }
                };

                await animation.RunAsync(this, null);
            }
        };
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}