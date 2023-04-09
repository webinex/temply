"use strict";(self.webpackChunkdocs=self.webpackChunkdocs||[]).push([[69],{3905:(e,n,t)=>{t.d(n,{Zo:()=>p,kt:()=>y});var a=t(7294);function r(e,n,t){return n in e?Object.defineProperty(e,n,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[n]=t,e}function i(e,n){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);n&&(a=a.filter((function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable}))),t.push.apply(t,a)}return t}function l(e){for(var n=1;n<arguments.length;n++){var t=null!=arguments[n]?arguments[n]:{};n%2?i(Object(t),!0).forEach((function(n){r(e,n,t[n])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):i(Object(t)).forEach((function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))}))}return e}function o(e,n){if(null==e)return{};var t,a,r=function(e,n){if(null==e)return{};var t,a,r={},i=Object.keys(e);for(a=0;a<i.length;a++)t=i[a],n.indexOf(t)>=0||(r[t]=e[t]);return r}(e,n);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(a=0;a<i.length;a++)t=i[a],n.indexOf(t)>=0||Object.prototype.propertyIsEnumerable.call(e,t)&&(r[t]=e[t])}return r}var c=a.createContext({}),s=function(e){var n=a.useContext(c),t=n;return e&&(t="function"==typeof e?e(n):l(l({},n),e)),t},p=function(e){var n=s(e.components);return a.createElement(c.Provider,{value:n},e.children)},u="mdxType",m={inlineCode:"code",wrapper:function(e){var n=e.children;return a.createElement(a.Fragment,{},n)}},d=a.forwardRef((function(e,n){var t=e.components,r=e.mdxType,i=e.originalType,c=e.parentName,p=o(e,["components","mdxType","originalType","parentName"]),u=s(t),d=r,y=u["".concat(c,".").concat(d)]||u[d]||m[d]||i;return t?a.createElement(y,l(l({ref:n},p),{},{components:t})):a.createElement(y,l({ref:n},p))}));function y(e,n){var t=arguments,r=n&&n.mdxType;if("string"==typeof e||r){var i=t.length,l=new Array(i);l[0]=d;var o={};for(var c in n)hasOwnProperty.call(n,c)&&(o[c]=n[c]);o.originalType=e,o[u]="string"==typeof e?e:r,l[1]=o;for(var s=2;s<i;s++)l[s]=t[s];return a.createElement.apply(null,l)}return a.createElement.apply(null,t)}d.displayName="MDXCreateElement"},7996:(e,n,t)=>{t.r(n),t.d(n,{assets:()=>c,contentTitle:()=>l,default:()=>m,frontMatter:()=>i,metadata:()=>o,toc:()=>s});var a=t(7462),r=(t(7294),t(3905));const i={title:"Localization",sidebar_position:4},l="Localization",o={unversionedId:"localization",id:"localization",title:"Localization",description:"Temply supports following localization types:",source:"@site/docs/localization.md",sourceDirName:".",slug:"/localization",permalink:"/temply/docs/localization",draft:!1,editUrl:"https://github.com/webinex/asky/tree/main/docs/docs/localization.md",tags:[],version:"current",sidebarPosition:4,frontMatter:{title:"Localization",sidebar_position:4},sidebar:"tutorialSidebar",previous:{title:"Profiles",permalink:"/temply/docs/profiles"},next:{title:"Configure Scriban",permalink:"/temply/docs/advanced-guides/configure-scriban"}},c={},s=[{value:"Key Localization",id:"key-localization",level:2},{value:"Resource Localization",id:"resource-localization",level:2},{value:"Custom Localization",id:"custom-localization",level:2}],p={toc:s},u="wrapper";function m(e){let{components:n,...t}=e;return(0,r.kt)(u,(0,a.Z)({},p,t,{components:n,mdxType:"MDXLayout"}),(0,r.kt)("h1",{id:"localization"},"Localization"),(0,r.kt)("p",null,"Temply supports following localization types:"),(0,r.kt)("ul",null,(0,r.kt)("li",{parentName:"ul"},"Key"),(0,r.kt)("li",{parentName:"ul"},"Resource"),(0,r.kt)("li",{parentName:"ul"},"Custom")),(0,r.kt)("h2",{id:"key-localization"},"Key Localization"),(0,r.kt)("p",null,"Key localization allows you to create multiple templates for each localization.  "),(0,r.kt)("p",null,"Create your custom implementation of ",(0,r.kt)("inlineCode",{parentName:"p"},"ITemplyKeyReplacer")),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre",className:"language-csharp"},'internal class MyKeyReplacer : ITemplyKeyReplacer\n{\n    private UserLangService _userLangService;\n\n    public Task<string> ReplaceAsync(TemplyArgs args)\n    {\n        var newKey = $"{args.Source.Value}_{_userLangService.Lang}";\n        return Task.FromResult(newKey);\n    }\n}\n')),(0,r.kt)("p",null,"And register it"),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre",className:"language-csharp"},'services.AddTemply(x => x\n    .AddProfile(p => p\n        .Add("hello_en", "Hello!")\n        .Add("hello_fr", "Salut!"))\n    .AddKeyReplacer<MyKeyReplacer>());\n')),(0,r.kt)("h2",{id:"resource-localization"},"Resource Localization"),(0,r.kt)("p",null,"Resource localization allows you to use localized string for each localization.",(0,r.kt)("br",{parentName:"p"}),"\n","Like: ",(0,r.kt)("inlineCode",{parentName:"p"},'We say: {{ localize "hello" values.name }}')," to print ",(0,r.kt)("inlineCode",{parentName:"p"},"We say: Hello John")," for ",(0,r.kt)("inlineCode",{parentName:"p"},"en")," locale and ",(0,r.kt)("inlineCode",{parentName:"p"},"We say: Salut John")," for ",(0,r.kt)("inlineCode",{parentName:"p"},"fr")," locale.  "),(0,r.kt)("p",null,"Create your custom implementation of ",(0,r.kt)("inlineCode",{parentName:"p"},"ITemplyLocalizationService")),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre",className:"language-csharp"},'public class LocalizationService : ITemplyLocalizationService\n{\n    private readonly UserLangService _langService;\n    \n    private readonly IDictionary<string, IDictionary<string, string>> _values =\n        new Dictionary<string, IDictionary<string, string>>\n        {\n            ["en"] = new Dictionary<string, string>\n            {\n                ["hello"] = "Hello {0}"\n            },\n            ["fr"] = new Dictionary<string, string>\n            {\n                ["hello"] = "Salut {0}"\n            }\n        };\n\n    public LocalizationService(UserLangService langService)\n    {\n        _langService = langService;\n    }\n\n    public string Get(string key, params object[] values)\n    {\n        return string.Format(_values[_langService.Lang][key], values);\n    }\n}\n')),(0,r.kt)("p",null,"And register it"),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre",className:"language-csharp"},"services.AddTemply(x => x\n    .AddLocalization<LocalizationService>()))\n")),(0,r.kt)("p",null,"It will expose ",(0,r.kt)("inlineCode",{parentName:"p"},"localize")," function which would be always accessible in your templates"),(0,r.kt)("h2",{id:"custom-localization"},"Custom Localization"),(0,r.kt)("p",null,"See ",(0,r.kt)("a",{parentName:"p",href:"./advanced-guides/configure-scriban"},"Configure Scriban")," section"))}m.isMDXComponent=!0}}]);