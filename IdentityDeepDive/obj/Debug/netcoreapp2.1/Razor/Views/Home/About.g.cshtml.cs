#pragma checksum "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "796759d03b81a1380abeabe2d2359ffbd1bfd432"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\_ViewImports.cshtml"
using IdentityDeepDive;

#line default
#line hidden
#line 2 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\_ViewImports.cshtml"
using IdentityDeepDive.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"796759d03b81a1380abeabe2d2359ffbd1bfd432", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d81cd8cce6f709728be1808344dc1d8200cc139", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml"
  
    ViewData["Title"] = "About";

#line default
#line hidden
            BeginContext(41, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(46, 17, false);
#line 4 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(63, 11, true);
            WriteLiteral("</h2>\r\n<h3>");
            EndContext();
            BeginContext(75, 19, false);
#line 5 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(94, 103, true);
            WriteLiteral("</h3>\r\n\r\n<p>Use this area to provide additional information.</p>\r\n\r\n\r\n\r\n<h2>Authenticated!</h2>\r\n<ul>\r\n");
            EndContext();
#line 13 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml"
     foreach (var claim in User.Claims)
    {

#line default
#line hidden
            BeginContext(245, 20, true);
            WriteLiteral("        <li><strong>");
            EndContext();
            BeginContext(266, 10, false);
#line 15 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml"
               Write(claim.Type);

#line default
#line hidden
            EndContext();
            BeginContext(276, 11, true);
            WriteLiteral("</strong>: ");
            EndContext();
            BeginContext(288, 11, false);
#line 15 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml"
                                     Write(claim.Value);

#line default
#line hidden
            EndContext();
            BeginContext(299, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
#line 16 "C:\Users\amaty\source\repos\IdentityDeepDive\IdentityDeepDive\Views\Home\About.cshtml"
    }

#line default
#line hidden
            BeginContext(313, 7, true);
            WriteLiteral("</ul>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591