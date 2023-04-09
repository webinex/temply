using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Webinex.Temply.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Webinex.Temply
{
    public static class TemplyServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds temply services
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configure">Delegate to configure Temply</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddTemply(
            [NotNull] this IServiceCollection services,
            Action<ITemplyConfiguration> configure = null)
        {
            services = services ?? throw new ArgumentNullException(nameof(services));

            var configuration = TemplyConfiguration.GetOrCreate(services);
            configure?.Invoke(configuration);

            return services;
        }
    }
}