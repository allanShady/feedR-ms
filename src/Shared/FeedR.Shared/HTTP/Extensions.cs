using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.HTTP;

public static class Extensions
{
    public static IServiceCollection AddHttpApiClient<TInterface, TClient>(this IServiceCollection services)
        where TInterface : class where TClient : class, TInterface
    {
        services.AddSingleton<TInterface, TClient>();
        return services;
    }
}