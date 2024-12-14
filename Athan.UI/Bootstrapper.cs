using Athan.UI.Features.Prayers;
using Athan.UI.Features.Shell;
using Athan.UI.Features.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static class Bootstrapper
{
    public static IServiceProvider Create()
    {
        var services = new ServiceCollection();

        services.AddTransient<ViewModelConverter>();

        services.AddTransient<ShellView>();
        services.AddTransient<ShellViewModel>();

        services.AddTransient<Func<WelcomeViewModel, WelcomeView>>(_ => viewModel => new WelcomeView(viewModel));
        services.AddTransient<WelcomeViewModel>();

        services.AddTransient<Func<PrayerViewModel, PrayerView>>(_ => viewModel => new PrayerView(viewModel));
        services.AddTransient<PrayerViewModel>();

        services.AddTransient<LocationService>();

        return services.BuildServiceProvider();
    }
}