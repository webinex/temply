using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Webinex.Temply
{
    internal interface ITemplateRenderer
    {
        Task<string> RenderAsync(
            [NotNull] TemplyArgs args,
            [NotNull] string template);
    }
}