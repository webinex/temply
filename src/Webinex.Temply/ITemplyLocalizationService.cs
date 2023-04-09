namespace Webinex.Temply
{
    /// <summary>
    ///     Allows to use `localize` function in scriban templates.
    ///     Create your impl and register it <code>services.AddTemply(x =&gt; x.AddLocalization&lt;YourImpl&gt;())</code>
    /// </summary>
    public interface ITemplyLocalizationService
    {
        /// <summary>
        ///     Gets string by <paramref name="key"/>.
        ///     Can format when <paramref name="values"/> provided.  
        /// </summary>
        /// <param name="key">Localization resource key</param>
        /// <param name="values">Formatting values</param>
        /// <returns>Localized string</returns>
        string Get(string key, params object[] values);
    }
}