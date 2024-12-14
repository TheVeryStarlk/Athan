using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.UI;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddView<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
            TView,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
            TViewModel>
        (this IServiceCollection services) where TView : class where TViewModel : class
    {
        services.AddSingleton<TView>();
        services.AddSingleton<TViewModel>();

        return services;
    }
}