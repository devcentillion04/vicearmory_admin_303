#pragma checksum "D:\Project\ViceArmory\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e06b8b4a50a9db7b03ff38e7d83d7fa02de3607f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Dashboard_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Dashboard/Index.cshtml")]
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
#line 1 "D:\Project\ViceArmory\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\_ViewImports.cshtml"
using ViceArmory.CoreWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Project\ViceArmory\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\_ViewImports.cshtml"
using ViceArmory.CoreWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e06b8b4a50a9db7b03ff38e7d83d7fa02de3607f", @"/Areas/Admin/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a625c85766fb227e24190b7cab2c6c5b6a68195b", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Project\ViceArmory\ViceArmory\src\ViceArmory.CoreWeb\Areas\Admin\Views\Dashboard\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container-fluid mt--6"">
    <div class=""row"">
        <div class=""col-xl-8"">
            <div class=""card bg-default"">
                <div class=""card-header bg-transparent"">
                    <div class=""row align-items-center"">
                        <div class=""col"">
                            <h6 class=""text-light text-uppercase ls-1 mb-1"">Overview</h6>
                            <h5 class=""h3 text-white mb-0"">Sales value</h5>
                        </div>
                        <div class=""col"">
                            <ul class=""nav nav-pills justify-content-end"">
                                <li class=""nav-item mr-2 mr-md-0"" data-toggle=""chart"" data-target=""#chart-sales-dark"" data-update='{""data"":{""datasets"":[{""data"":[0, 20, 10, 30, 15, 40, 20, 60, 60]}]}}' data-prefix=""$"" data-suffix=""k"">
                                    <a href=""#"" class=""nav-link py-2 px-3 active"" data-toggle=""tab"">
                                        <span class=""d-none d-md-block"">");
            WriteLiteral(@"Month</span>
                                        <span class=""d-md-none"">M</span>
                                    </a>
                                </li>
                                <li class=""nav-item"" data-toggle=""chart"" data-target=""#chart-sales-dark"" data-update='{""data"":{""datasets"":[{""data"":[0, 20, 5, 25, 10, 30, 15, 40, 40]}]}}' data-prefix=""$"" data-suffix=""k"">
                                    <a href=""#"" class=""nav-link py-2 px-3"" data-toggle=""tab"">
                                        <span class=""d-none d-md-block"">Week</span>
                                        <span class=""d-md-none"">W</span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class=""card-body"">
                    <!-- Chart -->
                    <div class=""chart"">
                        <!-- Chart wrapper -->
              ");
            WriteLiteral(@"          <canvas id=""chart-sales-dark"" class=""chart-canvas""></canvas>
                    </div>
                </div>
            </div>
        </div>
        <div class=""col-xl-4"">
            <div class=""card"">
                <div class=""card-header bg-transparent"">
                    <div class=""row align-items-center"">
                        <div class=""col"">
                            <h6 class=""text-uppercase text-muted ls-1 mb-1"">Performance</h6>
                            <h5 class=""h3 mb-0"">Total orders</h5>
                        </div>
                    </div>
                </div>
                <div class=""card-body"">
                    <!-- Chart -->
                    <div class=""chart"">
                        <canvas id=""chart-bars"" class=""chart-canvas""></canvas>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-xl-8"">
            <div class=""card"">
           ");
            WriteLiteral(@"     <div class=""card-header border-0"">
                    <div class=""row align-items-center"">
                        <div class=""col"">
                            <h3 class=""mb-0"">Page visits</h3>
                        </div>
                        <div class=""col text-right"">
                            <a href=""#!"" class=""btn btn-sm btn-primary"">See all</a>
                        </div>
                    </div>
                </div>
                <div class=""table-responsive"">
                    <!-- Projects table -->
                    <table class=""table align-items-center table-flush"">
                        <thead class=""thead-light"">
                            <tr>
                                <th scope=""col"">Page name</th>
                                <th scope=""col"">Visitors</th>
                                <th scope=""col"">Unique users</th>
                                <th scope=""col"">Bounce rate</th>
                            </tr>
               ");
            WriteLiteral(@"         </thead>
                        <tbody>
                            <tr>
                                <th scope=""row"">
                                    /argon/
                                </th>
                                <td>
                                    4,569
                                </td>
                                <td>
                                    340
                                </td>
                                <td>
                                    <i class=""fas fa-arrow-up text-success mr-3""></i> 46,53%
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    /argon/index.html
                                </th>
                                <td>
                                    3,985
                                </td>
                                <td>
                        ");
            WriteLiteral(@"            319
                                </td>
                                <td>
                                    <i class=""fas fa-arrow-down text-warning mr-3""></i> 46,53%
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    /argon/charts.html
                                </th>
                                <td>
                                    3,513
                                </td>
                                <td>
                                    294
                                </td>
                                <td>
                                    <i class=""fas fa-arrow-down text-warning mr-3""></i> 36,49%
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    /argon/tables.h");
            WriteLiteral(@"tml
                                </th>
                                <td>
                                    2,050
                                </td>
                                <td>
                                    147
                                </td>
                                <td>
                                    <i class=""fas fa-arrow-up text-success mr-3""></i> 50,87%
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    /argon/profile.html
                                </th>
                                <td>
                                    1,795
                                </td>
                                <td>
                                    190
                                </td>
                                <td>
                                    <i class=""fas fa-arrow-down text-danger mr-3");
            WriteLiteral(@"""></i> 46,53%
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class=""col-xl-4"">
            <div class=""card"">
                <div class=""card-header border-0"">
                    <div class=""row align-items-center"">
                        <div class=""col"">
                            <h3 class=""mb-0"">Social traffic</h3>
                        </div>
                        <div class=""col text-right"">
                            <a href=""#!"" class=""btn btn-sm btn-primary"">See all</a>
                        </div>
                    </div>
                </div>
                <div class=""table-responsive"">
                    <!-- Projects table -->
                    <table class=""table align-items-center table-flush"">
                        <thead class=""thead-light"">
                            <tr>
                   ");
            WriteLiteral(@"             <th scope=""col"">Referral</th>
                                <th scope=""col"">Visitors</th>
                                <th scope=""col""></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope=""row"">
                                    Facebook
                                </th>
                                <td>
                                    1,480
                                </td>
                                <td>
                                    <div class=""d-flex align-items-center"">
                                        <span class=""mr-2"">60%</span>
                                        <div>
                                            <div class=""progress"">
                                                <div class=""progress-bar bg-gradient-danger"" role=""progressbar"" aria-valuenow=""60"" aria-valuemin=""0"" aria-valuemax=""100"" s");
            WriteLiteral(@"tyle=""width: 60%;""></div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    Facebook
                                </th>
                                <td>
                                    5,480
                                </td>
                                <td>
                                    <div class=""d-flex align-items-center"">
                                        <span class=""mr-2"">70%</span>
                                        <div>
                                            <div class=""progress"">
                                                <div class=""progress-bar bg-gradient-success"" role=""progressbar"" aria-valuenow=""70"" aria-valuemin=""0"" aria-valuemax=""100"" style=""width: 70%;""");
            WriteLiteral(@"></div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    Google
                                </th>
                                <td>
                                    4,807
                                </td>
                                <td>
                                    <div class=""d-flex align-items-center"">
                                        <span class=""mr-2"">80%</span>
                                        <div>
                                            <div class=""progress"">
                                                <div class=""progress-bar bg-gradient-primary"" role=""progressbar"" aria-valuenow=""80"" aria-valuemin=""0"" aria-valuemax=""100"" style=""width: 80%;""></div>
           ");
            WriteLiteral(@"                                 </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    Instagram
                                </th>
                                <td>
                                    3,678
                                </td>
                                <td>
                                    <div class=""d-flex align-items-center"">
                                        <span class=""mr-2"">75%</span>
                                        <div>
                                            <div class=""progress"">
                                                <div class=""progress-bar bg-gradient-info"" role=""progressbar"" aria-valuenow=""75"" aria-valuemin=""0"" aria-valuemax=""100"" style=""width: 75%;""></div>
                               ");
            WriteLiteral(@"             </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope=""row"">
                                    twitter
                                </th>
                                <td>
                                    2,645
                                </td>
                                <td>
                                    <div class=""d-flex align-items-center"">
                                        <span class=""mr-2"">30%</span>
                                        <div>
                                            <div class=""progress"">
                                                <div class=""progress-bar bg-gradient-warning"" role=""progressbar"" aria-valuenow=""30"" aria-valuemin=""0"" aria-valuemax=""100"" style=""width: 30%;""></div>
                                            </div>");
            WriteLiteral(@"
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

");
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
