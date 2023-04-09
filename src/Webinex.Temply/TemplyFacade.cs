using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webinex.Temply.Resources;

namespace Webinex.Temply
{
    internal class TemplyFacade : ITemply
    {
        private readonly IEnumerable<ITemplyResourceLoader> _resourceLoaders;
        private readonly ITemplyKeyReplacer _keyReplacer;
        private readonly ITemplateRenderer _renderer;

        public TemplyFacade(
            IEnumerable<ITemplyResourceLoader> resourceLoaders,
            ITemplyKeyReplacer keyReplacer,
            ITemplateRenderer renderer)
        {
            _resourceLoaders = resourceLoaders;
            _keyReplacer = keyReplacer;
            _renderer = renderer;
        }

        public async Task<string> RenderAsync(TemplyArgs args)
        {
            args = args ?? throw new ArgumentNullException(nameof(args));
            
            if (args.Source.IsKeyed)
                args = args.WithKey(await _keyReplacer.ReplaceAsync(args));

            var template = await TemplateByKeyAsync(args.Source);
            var result = await _renderer.RenderAsync(args, template);
            return result ?? throw new InvalidOperationException("Renderer result should not be null");
        }

        public async Task<string> TemplateByKeyAsync(string key)
        {
            key = key ?? throw new ArgumentNullException(nameof(key));
            
            foreach (var resourceLoader in _resourceLoaders)
            {
                var result = await resourceLoader.ByKeyAsync(key);
                if (result.Success) return result.Value;
            }

            return null;
        }

        private async Task<string> TemplateByKeyAsync(TemplySource templySource)
        {
            if (!templySource.IsKeyed)
                return templySource.Value;

            var result = await TemplateByKeyAsync(templySource.Value);
            return result ?? throw new InvalidOperationException($"Given key not found {templySource.Value}");
        }
    }
}