using Athan.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

// To feed the design time property of each view that needs it
internal sealed class ViewModelLocator
{
    public static DashboardViewModel DashboardViewModel => App.Current.Services.GetRequiredService<DashboardViewModel>();

    public static LocationViewModel LocationViewModel => App.Current.Services.GetRequiredService<LocationViewModel>();

    public static OfflineViewModel OfflineViewModel => App.Current.Services.GetRequiredService<OfflineViewModel>();

    public static PrayersViewModel PrayersViewModel => App.Current.Services.GetRequiredService<PrayersViewModel>();

    public static SettingsViewModel SettingsViewModel => App.Current.Services.GetRequiredService<SettingsViewModel>();

    public static ShellViewModel ShellViewModel => App.Current.Services.GetRequiredService<ShellViewModel>();
}