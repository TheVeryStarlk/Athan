using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using Avalonia.Styling;

namespace Athan.Avalonia.Extensions;

public static class AnimatableExtension
{
    public static async Task AnimateAsync(this Animatable animatable)
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
                        new Setter(Visual.OpacityProperty, 0.0),
                        new Setter(TranslateTransform.YProperty, 50D),
                    }
                },
                new KeyFrame()
                {
                    KeyTime = TimeSpan.FromMilliseconds(750),
                    Setters =
                    {
                        new Setter(Visual.OpacityProperty, 1),
                        new Setter(TranslateTransform.YProperty, 0D),
                    }
                }
            }
        };

        await animation.RunAsync(animatable, null);
    }
}