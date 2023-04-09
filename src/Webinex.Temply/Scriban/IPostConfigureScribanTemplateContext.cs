using System.Threading.Tasks;
using Scriban;

namespace Webinex.Temply.Scriban
{
    public interface IPostConfigureScribanTemplateContext
    {
        Task<TemplateContext> ConfigureAsync(TemplateContext context);
    }
}