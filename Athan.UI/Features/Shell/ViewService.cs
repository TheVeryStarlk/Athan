using Athan.UI.Features.Prayers;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Tasbih;
using System;
using System.ComponentModel;

namespace Athan.UI.Features.Shell;

internal sealed class ViewService
{
    public Type For(INotifyPropertyChanged? instance)   
    {
        return instance switch
        {
            PrayersViewModel => typeof(PrayersView),
            TasbihViewModel => typeof(TasbihView),
            SettingsViewModel => typeof(SettingsView),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}