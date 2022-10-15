using Athan.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia;

// To feed the design time property of each view that needs it
internal sealed class ViewModelLocator
{
    public ShellViewModel ShellViewModel => App.Current.Services.GetRequiredService<ShellViewModel>();

    public OfflineViewModel OfflineViewModel => App.Current.Services.GetRequiredService<OfflineViewModel>();

    public LocationViewModel LocationViewModel => App.Current.Services.GetRequiredService<LocationViewModel>();

    public DashboardViewModel DashboardViewModel => App.Current.Services.GetRequiredService<DashboardViewModel>();
}