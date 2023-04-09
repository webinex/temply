using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Webinex.Temply.Resources
{
    public interface ITemplyResourceLoader
    {
        Task<TemplyResourceResult> ByKeyAsync([NotNull] string key);
    }
}