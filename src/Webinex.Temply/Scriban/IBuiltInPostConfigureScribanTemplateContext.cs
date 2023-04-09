using Scriban;

namespace Webinex.Temply.Scriban
{
    internal interface IBuiltInPostConfigureScribanTemplateContext
    {
        TemplateContext Configure(TemplateContext context);
    }
}