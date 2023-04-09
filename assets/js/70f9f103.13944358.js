"use strict";(self.webpackChunkdocs=self.webpackChunkdocs||[]).push([[921],{3905:(e,t,r)=>{r.d(t,{Zo:()=>d,kt:()=>f});var n=r(7294);function a(e,t,r){return t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function l(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,n)}return r}function i(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?l(Object(r),!0).forEach((function(t){a(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):l(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function o(e,t){if(null==e)return{};var r,n,a=function(e,t){if(null==e)return{};var r,n,a={},l=Object.keys(e);for(n=0;n<l.length;n++)r=l[n],t.indexOf(r)>=0||(a[r]=e[r]);return a}(e,t);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(e);for(n=0;n<l.length;n++)r=l[n],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(a[r]=e[r])}return a}var p=n.createContext({}),s=function(e){var t=n.useContext(p),r=t;return e&&(r="function"==typeof e?e(t):i(i({},t),e)),r},d=function(e){var t=s(e.components);return n.createElement(p.Provider,{value:t},e.children)},m="mdxType",u={inlineCode:"code",wrapper:function(e){var t=e.children;return n.createElement(n.Fragment,{},t)}},c=n.forwardRef((function(e,t){var r=e.components,a=e.mdxType,l=e.originalType,p=e.parentName,d=o(e,["components","mdxType","originalType","parentName"]),m=s(r),c=a,f=m["".concat(p,".").concat(c)]||m[c]||u[c]||l;return r?n.createElement(f,i(i({ref:t},d),{},{components:r})):n.createElement(f,i({ref:t},d))}));function f(e,t){var r=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var l=r.length,i=new Array(l);i[0]=c;var o={};for(var p in t)hasOwnProperty.call(t,p)&&(o[p]=t[p]);o.originalType=e,o[m]="string"==typeof e?e:a,i[1]=o;for(var s=2;s<l;s++)i[s]=r[s];return n.createElement.apply(null,i)}return n.createElement.apply(null,r)}c.displayName="MDXCreateElement"},4870:(e,t,r)=>{r.r(t),r.d(t,{assets:()=>p,contentTitle:()=>i,default:()=>u,frontMatter:()=>l,metadata:()=>o,toc:()=>s});var n=r(7462),a=(r(7294),r(3905));const l={sidebar_position:3,title:"Profiles"},i="Profiles",o={unversionedId:"profiles",id:"profiles",title:"Profiles",description:"Create & Register",source:"@site/docs/profiles.md",sourceDirName:".",slug:"/profiles",permalink:"/temply/docs/profiles",draft:!1,editUrl:"https://github.com/webinex/asky/tree/main/docs/docs/profiles.md",tags:[],version:"current",sidebarPosition:3,frontMatter:{sidebar_position:3,title:"Profiles"},sidebar:"tutorialSidebar",previous:{title:"Getting Started",permalink:"/temply/docs/getting-started"},next:{title:"Localization",permalink:"/temply/docs/localization"}},p={},s=[{value:"Create &amp; Register",id:"create--register",level:2},{value:"Profile Type",id:"profile-type",level:3},{value:"Profile Instance",id:"profile-instance",level:3},{value:"Profile Delegate",id:"profile-delegate",level:3},{value:"Builder",id:"builder",level:2}],d={toc:s},m="wrapper";function u(e){let{components:t,...r}=e;return(0,a.kt)(m,(0,n.Z)({},d,r,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("h1",{id:"profiles"},"Profiles"),(0,a.kt)("h2",{id:"create--register"},"Create & Register"),(0,a.kt)("admonition",{title:"Key duplication",type:"info"},(0,a.kt)("p",{parentName:"admonition"},"You might be aware, if you add same key (in one or different profiles), first added template would be used.")),(0,a.kt)("p",null,"Temply allows you to define profiles as:"),(0,a.kt)("h3",{id:"profile-type"},"Profile Type"),(0,a.kt)("p",null,"Profile might have parameterless constructor."),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-csharp"},"public class MyTemplyProfile : TemplyProfile\n{\n    public override void Configure(TemplyProfileBuilder builder)\n    {\n      // ...\n    }\n}\n\nservices.AddTemply(x => x.AddProfile<MyTemplyProfile>());\n")),(0,a.kt)("h3",{id:"profile-instance"},"Profile Instance"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-csharp"},"public class MyTemplyProfile : TemplyProfile\n{\n    private readonly IWebHostEnvironment _environment;\n\n    public MyTemplyProfile(IWebHostEnvironment environment)\n    {\n        _environment = environment;\n    }\n\n    public override void Configure(TemplyProfileBuilder builder)\n    {\n      // ...\n    }\n}\n\nservices.AddTemply(x => x.AddProfile(new MyTemplyProfile(_environment)));\n")),(0,a.kt)("h3",{id:"profile-delegate"},"Profile Delegate"),(0,a.kt)("pre",null,(0,a.kt)("code",{parentName:"pre",className:"language-csharp"},"services.AddTemply(x => x.AddProfile(profile => /* .... */));\n")),(0,a.kt)("h2",{id:"builder"},"Builder"),(0,a.kt)("p",null,"Profile builder allows you to configure your template. It has following methods:"),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"Add")," - adds in memory resource")),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"AddFile")," - adds file content as resource (when template key not specified, uses file name without extension as Template key)")),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"AddYaml")," - adds YAML entries as resources"),(0,a.kt)("pre",{parentName:"li"},(0,a.kt)("code",{parentName:"pre",className:"language-yaml"},"users:\n  hello: Hello {{ values.name }}\n  bye: Bye {{ values.name }}\n")),(0,a.kt)("p",{parentName:"li"},"would be added as ",(0,a.kt)("inlineCode",{parentName:"p"},"users.hello")," and ",(0,a.kt)("inlineCode",{parentName:"p"},"users.bye")," template keys")),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"AddJson")," - adds JSON entries as resources"),(0,a.kt)("pre",{parentName:"li"},(0,a.kt)("code",{parentName:"pre",className:"language-json"},'{\n  "users": {\n    "hello": "Hello {{ values.name }}",\n    "bye": "Bye {{ values.name }}"\n  }\n}\n')),(0,a.kt)("p",{parentName:"li"},"would be added as ",(0,a.kt)("inlineCode",{parentName:"p"},"users.hello")," and ",(0,a.kt)("inlineCode",{parentName:"p"},"users.bye")," template keys")),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"AddDir")," - adds directory files (not recursive) as templates"),(0,a.kt)("p",{parentName:"li"},(0,a.kt)("inlineCode",{parentName:"p"},".html, .htm, .txt, .md")," - files added using ",(0,a.kt)("inlineCode",{parentName:"p"},"AddFile")," call",(0,a.kt)("br",{parentName:"p"}),"\n","",(0,a.kt)("inlineCode",{parentName:"p"},".yaml, .yml")," - files added using ",(0,a.kt)("inlineCode",{parentName:"p"},"AddYaml")," call",(0,a.kt)("br",{parentName:"p"}),"\n","",(0,a.kt)("inlineCode",{parentName:"p"},".json")," - files added using ",(0,a.kt)("inlineCode",{parentName:"p"},"AddJson")," call"),(0,a.kt)("p",{parentName:"li"},"Note: ",(0,a.kt)("inlineCode",{parentName:"p"},"noCache")," option would not affect newly added files (they would ignore)")),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("p",{parentName:"li"},(0,a.kt)("strong",{parentName:"p"},"Add(Resource Loader)")," - adds built-in or custom implementation of resource loader"))))}u.isMDXComponent=!0}}]);