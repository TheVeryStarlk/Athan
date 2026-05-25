using Microsoft.UI.Xaml.Data;
using Siraj.Features.Prayers.Calculation;
using System;

namespace Siraj.Features.Prayers;

internal sealed partial class UpcomingToRowConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return (int) (PrayerKind) value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new InvalidOperationException();
    }
}