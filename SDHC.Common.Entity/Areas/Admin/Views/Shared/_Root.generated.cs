﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using WebSQL;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/_Root.cshtml")]
    public partial class _Areas_Admin_Views_Shared__Root_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Admin_Views_Shared__Root_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n");

            
            #line 5 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
  
            
            #line default
            #line hidden
            
            #line 5 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
     Html.RenderPartial("_head"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 6 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
  
            
            #line default
            #line hidden
            
            #line 6 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
     Html.RenderPartial("_css"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n</head>\r\n\r\n<body");

WriteLiteral(" class=\"theme-indigo light layout-fixed\"");

WriteLiteral(">\r\n  <div");

WriteLiteral(" class=\"wrapper\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"page-loader-wrapper\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"loader\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"sk-wave\"");

WriteLiteral(">\r\n          <div");

WriteLiteral(" class=\"sk-rect sk-rect1 bg-cyan\"");

WriteLiteral("></div>\r\n          <div");

WriteLiteral(" class=\"sk-rect sk-rect2 bg-cyan\"");

WriteLiteral("></div>\r\n          <div");

WriteLiteral(" class=\"sk-rect sk-rect3 bg-cyan\"");

WriteLiteral("></div>\r\n          <div");

WriteLiteral(" class=\"sk-rect sk-rect4 bg-cyan\"");

WriteLiteral("></div>\r\n          <div");

WriteLiteral(" class=\"sk-rect sk-rect5 bg-cyan\"");

WriteLiteral("></div>\r\n        </div>\r\n        <p>Please wait...</p>\r\n      </div>\r\n    </div>\r" +
"\n    <!-- top navbar-->\r\n    <header");

WriteLiteral(" class=\"topnavbar-wrapper\"");

WriteLiteral(">\r\n      <nav");

WriteLiteral(" role=\"navigation\"");

WriteLiteral(" class=\"navbar topnavbar\"");

WriteLiteral(">\r\n        <!-- START navbar header-->\r\n        <div");

WriteLiteral(" class=\"navbar-header\"");

WriteLiteral(">\r\n          <a");

WriteAttribute("href", Tuple.Create(" href=\"", 849), Tuple.Create("\"", 916)
            
            #line 28 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
, Tuple.Create(Tuple.Create("", 856), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Dashboard",new { @area = G.AdminPath })
            
            #line default
            #line hidden
, 856), false)
);

WriteLiteral(" class=\"navbar-brand\"");

WriteLiteral(">\r\n              <br/>\r\n              Admin\r\n            ");

WriteLiteral("\r\n          </a>\r\n        </div>\r\n        <!-- END navbar header-->\r\n        <!--" +
" START Nav wrapper-->\r\n        <div");

WriteLiteral(" class=\"nav-wrapper\"");

WriteLiteral(">\r\n          <!-- START Left navbar-->\r\n          <ul");

WriteLiteral(" class=\"nav navbar-nav\"");

WriteLiteral(">\r\n            <li>\r\n              <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" data-trigger-resize=\"\"");

WriteLiteral(" data-toggle-state=\"aside-collapsed\"");

WriteLiteral(" class=\"hidden-xs\"");

WriteLiteral(">\r\n                <em");

WriteLiteral(" class=\"material-icons\"");

WriteLiteral(">menu</em>\r\n              </a>\r\n              <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" data-toggle-state=\"aside-toggled\"");

WriteLiteral(" data-no-persist=\"true\"");

WriteLiteral(" class=\"visible-xs sidebar-toggle\"");

WriteLiteral(">\r\n                <em");

WriteLiteral(" class=\"material-icons\"");

WriteLiteral(">menu</em>\r\n              </a>\r\n            </li>\r\n          </ul>\r\n          <!-" +
"- END Left navbar-->\r\n          <!-- START Right Navbar-->\r\n          <ul");

WriteLiteral(" class=\"nav navbar-nav navbar-right\"");

WriteLiteral("></ul>\r\n          <!-- #END# Right Navbar-->\r\n        </div>\r\n      </nav>\r\n     " +
" <!-- END Top Navbar-->\r\n    </header>\r\n    <!-- sidebar-->\r\n");

            
            #line 62 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
    
            
            #line default
            #line hidden
            
            #line 62 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
       Html.RenderPartial("_LeftSideBar");
            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 63 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n    <footer>\r\n      <span>");

            
            #line 65 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
       Write(Html.Raw(G.AdminCopyright.Text("&copy; spxus admin")));

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n    </footer>\r\n  </div>\r\n\r\n");

            
            #line 69 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
  
            
            #line default
            #line hidden
            
            #line 69 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
     Html.RenderPartial("_js"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n  <script>\r\n      $(\".form_datetime\").datepicker({\r\n          format: \'yyyy-mm-" +
"dd\',\r\n          fontAwesome: !0,\r\n          autoclose: true,\r\n          pickerPo" +
"sition: \"top-right\"\r\n\r\n      });\r\n  </script>\r\n");

WriteLiteral("  ");

            
            #line 79 "..\..\Areas\Admin\Views\Shared\_Root.cshtml"
Write(RenderSection("script", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n</body>\r\n</html>");

        }
    }
}
#pragma warning restore 1591
