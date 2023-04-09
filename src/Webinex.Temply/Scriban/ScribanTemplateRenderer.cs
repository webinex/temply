using System.Threading.Tasks;
using Scriban;

namespace Webinex.Temply.Scriban
{
    internal class ScribanTemplateRenderer : ITemplateRenderer
    {
        private readonly IScribanTemplateContextFactory _contextFactory;

        public ScribanTemplateRenderer(IScribanTemplateContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<string> RenderAsync(TemplyArgs args, string template)
        {
            var scribanTemplate = Template.Parse(template);
            var context = await _contextFactory.CreateAsync(args);
            return await scribanTemplate.RenderAsync(context);
        }
    }
}