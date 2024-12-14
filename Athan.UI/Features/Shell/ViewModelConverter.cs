using Athan.UI.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Data;

namespace Athan.UI.Features.Shell;

internal sealed partial class ViewModelConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value switch
        {
            WelcomeViewModel => App.Services.GetRequiredService<WelcomeView>(),
            _ => throw new ArgumentException("Unknown type.")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new InvalidOperationException();
    }
}