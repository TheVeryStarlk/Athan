using Microsoft.Extensions.DependencyInjection;

namespace Athan.Desktop;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFactory<TService, TArgument>(this IServiceCollection services) where TService : class
    {
        services.AddScoped<Func<TArgument, TService>>(provider => argument => ActivatorUtilities.CreateInstance<TService>(provider, argument!));
        return services;
    }
}