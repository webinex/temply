using System;
using System.Threading.Tasks;
using Scriban;

namespace Webinex.Temply.Scriban
{
    internal class PostConfigureScribanTemplateContextDelegate : IPostConfigureScribanTemplateContext
    {
        private readonly Action<TemplateContext> _configure;

        public PostConfigureScribanTemplateContextDelegate(Action<TemplateContext> configure)
        {
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
        }

        public Task<TemplateContext> ConfigureAsync(TemplateContext context)
        {
            _configure(context);
            return Task.FromResult(context);
        }
    }
}