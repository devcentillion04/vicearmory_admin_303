#pragma checksum "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Weeklyads\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9fcfe9ce83da4b178f9f913f7e05f0a51074b0f3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Weeklyads_Create), @"mvc.1.0.view", @"/Areas/Admin/Views/Weeklyads/Create.cshtml")]
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
#nullable restore
#line 1 "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\_ViewImports.cshtml"
using ViceArmory.CoreWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\_ViewImports.cshtml"
using ViceArmory.CoreWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9fcfe9ce83da4b178f9f913f7e05f0a51074b0f3", @"/Areas/Admin/Views/Weeklyads/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a625c85766fb227e24190b7cab2c6c5b6a68195b", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Weeklyads_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ViceArmory.RequestObject.Product.ProductRequestDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Weeklyads\Create.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_ALayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Weekly Ads</h1>\r\n\r\n<hr />\r\n<br />\r\n<div class=\"row\">\r\n    <div class=\"col-md-4\">\r\n");
#nullable restore
#line 14 "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Weeklyads\Create.cshtml"
         using (Html.BeginForm("Create", "Weeklyads", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span class=\"text-danger\">");
#nullable restore
#line 16 "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Weeklyads\Create.cshtml"
                                 Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            <div>\r\n                ");
#nullable restore
#line 18 "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Weeklyads\Create.cshtml"
           Write(Html.TextBox("file", "", new { type = "file" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <br />\r\n                <input class=\"btn btn-primary\" type=\"submit\" value=\"Upload\" />\r\n                \r\n            </div>\r\n");
#nullable restore
#line 23 "D:\centillion\project\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Weeklyads\Create.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n<br />\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9fcfe9ce83da4b178f9f913f7e05f0a51074b0f35362", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ViceArmory.RequestObject.Product.ProductRequestDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
