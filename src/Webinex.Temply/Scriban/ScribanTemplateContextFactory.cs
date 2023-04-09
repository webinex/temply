using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Scriban;
using Scriban.Runtime;

namespace Webinex.Temply.Scriban
{
    /// <summary>
    ///     Gives ability to configure template context.
    ///     Such as additional services or methods that can be used in template.
    /// </summary>
    public interface IScribanTemplateContextFactory
    {
        /// <summary>
        ///     Creates <see cref="TemplateContext"/> based on <paramref name="args"/>
        /// </summary>
        /// <param name="args">Temply args</param>
        /// <returns><see cref="TemplateContext"/></returns>
        Task<TemplateContext> CreateAsync([NotNull] TemplyArgs args);
    }

    internal class DefaultScribanTemplateContextFactory : IScribanTemplateContextFactory
    {
        private readonly IPostConfigureScribanTemplateContext _configure;
        private readonly IEnumerable<IBuiltInPostConfigureScribanTemplateContext> _builtInConfigures;

        public DefaultScribanTemplateContextFactory(
            IPostConfigureScribanTemplateContext configure,
            IEnumerable<IBuiltInPostConfigureScribanTemplateContext> builtInConfigures)
        {
            _configure = configure;
            _builtInConfigures = builtInConfigures;
        }

        public async Task<TemplateContext> CreateAsync(TemplyArgs args)
        {
            args = args ?? throw new ArgumentNullException(nameof(args));

            var scriptObject = new ScriptObject
            {
                ["values"] = args.Values,
            };

            var context = new TemplateContext();
            context.PushGlobal(scriptObject);
            
            foreach (var builtInConfigure in _builtInConfigures)
            {
                context = builtInConfigure.Configure(context);
            }

            await _configure.ConfigureAsync(context);
            return context;
        }
    }
}