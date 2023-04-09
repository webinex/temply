using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scriban;
using Webinex.Temply.Scriban;

namespace Webinex.Temply
{
    public interface ITemplyConfiguration
    {
        /// <summary>
        ///     Service collection. Useful for child packages.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        ///     Values useful in child packages to share data between calls.
        /// </summary>
        IDictionary<string, object> Values { get; }

        /// <summary>
        ///     Adds temply profile
        /// </summary>
        /// <param name="profile">Temply profile to add</param>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        ITemplyConfiguration AddProfile([NotNull] TemplyProfile profile);

        /// <summary>
        ///     Adds localization service. Which would be available as `localize` function
        /// </summary>
        /// <param name="type">Localization service type</param>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        ITemplyConfiguration AddLocalization(Type type);

        /// <summary>
        ///     Adds custom key replacer with scope lifetime
        /// </summary>
        /// <param name="type">Key replacer service type</param>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        ITemplyConfiguration AddKeyReplacer(Type type);
    }

    internal class TemplyConfiguration : ITemplyConfiguration
    {
        private TemplyConfiguration(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));

            services.TryAddScoped<ITemply, TemplyFacade>();
            services.TryAddSingleton<ITemplyKeyReplacer, EmptyKeyReplacer>();
            services.TryAddScoped<IScribanTemplateContextFactory, DefaultScribanTemplateContextFactory>();
            services.TryAddSingleton<IPostConfigureScribanTemplateContext>(
                new PostConfigureScribanTemplateContextDelegate(_ => { }));
            services.TryAddScoped<ITemplateRenderer, ScribanTemplateRenderer>();
        }

        public static TemplyConfiguration GetOrCreate(IServiceCollection services)
        {
            return services.SingleOrDefault(x =>
                           x.Lifetime == ServiceLifetime.Singleton && x.ServiceType == typeof(TemplyConfiguration))
                       ?.ImplementationInstance as TemplyConfiguration
                   ?? new TemplyConfiguration(services);
        }

        public IServiceCollection Services { get; }

        public IDictionary<string, object> Values { get; } = new Dictionary<string, object>();

        public ITemplyConfiguration AddProfile(TemplyProfile profile)
        {
            profile = profile ?? throw new ArgumentNullException(nameof(profile));
            profile.Configure(new TemplyProfileBuilder(this));
            return this;
        }

        public ITemplyConfiguration AddLocalization(Type type)
        {
            type = type ?? throw new ArgumentNullException(nameof(type));

            if (!typeof(ITemplyLocalizationService).IsAssignableFrom(type))
            {
                throw new ArgumentException(
                    $"Might be assignable to {nameof(ITemplyLocalizationService)}",
                    nameof(type));
            }

            Services.TryAddScoped(typeof(ITemplyLocalizationService), type);
            Services.AddScoped<
                IBuiltInPostConfigureScribanTemplateContext,
                LocalizationBuiltInPostConfigureScribanTemplateContext>();

            return this;
        }

        public ITemplyConfiguration AddKeyReplacer(Type type)
        {
            type = type ?? throw new ArgumentNullException(nameof(type));

            if (!typeof(ITemplyKeyReplacer).IsAssignableFrom(type))
            {
                throw new ArgumentException(
                    $"Might be assignable to {nameof(ITemplyKeyReplacer)}",
                    nameof(type));
            }

            Services.AddScoped(typeof(ITemplyKeyReplacer), type);
            return this;
        }
    }

    public static class TemplyConfigurationExtensions
    {
        /// <summary>
        ///     Adds temply profile
        /// </summary>
        /// <param name="configuration"><see cref="ITemplyConfiguration"/></param>
        /// <typeparam name="T">Type of profile</typeparam>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        public static ITemplyConfiguration AddProfile<T>([NotNull] this ITemplyConfiguration configuration)
            where T : TemplyProfile
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            var instance = Activator.CreateInstance<T>();
            return configuration.AddProfile(instance);
        }

        /// <summary>
        ///     Adds temply profile
        /// </summary>
        /// <param name="configuration"><see cref="ITemplyConfiguration"/></param>
        /// <param name="configure">Temply profile configuration delegate</param>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        public static ITemplyConfiguration AddProfile(
            [NotNull] this ITemplyConfiguration configuration,
            [NotNull] Action<TemplyProfileBuilder> configure)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            configure = configure ?? throw new ArgumentNullException(nameof(configure));

            return configuration.AddProfile(new DelegateTemplyProfile(configure));
        }

        /// <summary>
        ///     Adds ability to configure scriban template context
        /// </summary>
        /// <param name="configuration"><see cref="ITemplyConfiguration"/></param>
        /// <param name="type"><see cref="IPostConfigureScribanTemplateContext"/> implementation type</param>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        public static ITemplyConfiguration AddPostConfigureScriban(
            [NotNull] this ITemplyConfiguration configuration,
            [NotNull] Type type)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            type = type ?? throw new ArgumentNullException(nameof(type));

            if (!typeof(IPostConfigureScribanTemplateContext).IsAssignableFrom(type))
            {
                throw new ArgumentException(
                    $"Might be assignable from {nameof(IPostConfigureScribanTemplateContext)}",
                    nameof(type));
            }

            configuration.Services.AddScoped(
                typeof(IPostConfigureScribanTemplateContext),
                type);

            return configuration;
        }

        /// <summary>
        ///     Adds ability to configure scriban template context
        /// </summary>
        /// <param name="configuration"><see cref="ITemplyConfiguration"/></param>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        public static ITemplyConfiguration AddPostConfigureScriban<T>(
            [NotNull] this ITemplyConfiguration configuration)
            where T : IPostConfigureScribanTemplateContext
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            return configuration.AddPostConfigureScriban(typeof(T));
        }

        /// <summary>
        ///     Adds ability to configure scriban template context
        /// </summary>
        /// <param name="configuration"><see cref="ITemplyConfiguration"/></param>
        /// <param name="configure">Configuration delegate</param>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        public static ITemplyConfiguration AddPostConfigureScriban(
            [NotNull] this ITemplyConfiguration configuration,
            [NotNull] Action<TemplateContext> configure)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            configure = configure ?? throw new ArgumentNullException(nameof(configure));

            configuration.Services.AddSingleton<IPostConfigureScribanTemplateContext>(
                new PostConfigureScribanTemplateContextDelegate(configure));

            return configuration;
        }

        /// <summary>
        ///     Adds localization service. Which would be available as `localize` function
        /// </summary>
        /// <typeparam name="T">Localization service type</typeparam>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        public static ITemplyConfiguration AddLocalization<T>(
            [NotNull] this ITemplyConfiguration configuration)
            where T : ITemplyLocalizationService
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            return configuration.AddLocalization(typeof(T));
        }

        /// <summary>
        ///     Adds key replacer service with scoped lifetime
        /// </summary>
        /// <typeparam name="T">Localization service type</typeparam>
        /// <returns><see cref="ITemplyConfiguration"/></returns>
        public static ITemplyConfiguration AddKeyReplacer<T>(
            [NotNull] this ITemplyConfiguration configuration)
            where T : ITemplyKeyReplacer
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            return configuration.AddKeyReplacer(typeof(T));
        }
    }
}