using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Webinex.Temply
{
    public interface ITemply
    {
        /// <summary>
        ///     Renders specified template arguments to string
        /// </summary>
        /// <param name="args">Specifies template source and values</param>
        /// <returns>Rendered text result</returns>
        Task<string> RenderAsync([NotNull] TemplyArgs args);

        /// <summary>
        ///     Returns template associated with given <paramref name="key"/>
        /// </summary>
        /// <param name="key">Key to get template</param>
        /// <returns>Template</returns>
        Task<string> TemplateByKeyAsync([NotNull] string key);
    }
}