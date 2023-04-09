using System;
using System.Linq;
using System.Threading.Tasks;
using Scriban;
using Scriban.Runtime;
using Scriban.Syntax;

namespace Webinex.Temply.Scriban
{
    internal class LocalizationBuiltInPostConfigureScribanTemplateContext
        : IBuiltInPostConfigureScribanTemplateContext
    {
        private readonly ITemplyLocalizationService _service;

        public LocalizationBuiltInPostConfigureScribanTemplateContext(ITemplyLocalizationService service)
        {
            _service = service;
        }

        public TemplateContext Configure(TemplateContext context)
        {
            context.BuiltinObject.SetValue("localize", new Function(_service), true);
            return context;
        }

        private class Function : IScriptCustomFunction
        {
            private readonly ITemplyLocalizationService _service;

            public Function(ITemplyLocalizationService service)
            {
                _service = service;
            }

            public int RequiredParameterCount => 1;
            public int ParameterCount => 2;
            public ScriptVarParamKind VarParamKind => ScriptVarParamKind.LastParameter;
            public Type ReturnType => typeof(string);

            public object Invoke(
                TemplateContext context,
                ScriptNode callerContext,
                ScriptArray arguments,
                ScriptBlockStatement blockStatement)
            {
                if (arguments.Count == 0)
                    throw new ScriptRuntimeException(callerContext.Span,
                        "Expecting at least the localization key for a function `localize`");

                var key = context.ObjectToString(arguments[0]);
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ScriptRuntimeException(callerContext.Span,
                        "Localization key might not be null or whitespace");
                }

                var varArgsArguments = (ScriptArray) arguments[1];
                var parameters = varArgsArguments.ToArray();
                return _service.Get(key, parameters);
            }

            public ScriptParameterInfo GetParameterInfo(int index)
            {
                return index == 0 ? new ScriptParameterInfo(typeof(string), "key") : new ScriptParameterInfo(typeof(object), $"param{index}");
            }

            public ValueTask<object> InvokeAsync(TemplateContext context, ScriptNode callerContext,
                ScriptArray arguments,
                ScriptBlockStatement blockStatement)
            {
                return new ValueTask<object>(Invoke(context, callerContext, arguments, blockStatement));
            }
        }
    }
}