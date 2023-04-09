using System.Threading.Tasks;

namespace Webinex.Temply.Resources
{
    public interface ITemplyTextSource
    {
        Task<string> ReadAsync();
    }
}