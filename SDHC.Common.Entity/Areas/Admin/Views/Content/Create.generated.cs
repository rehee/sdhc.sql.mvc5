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
    
    #line 1 "..\..\Areas\Admin\Views\Content\Create.cshtml"
    using SDHC.Common.Entity.Models;
    
    #line default
    #line hidden
    using WebSQL;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Content/Create.cshtml")]
    public partial class _Areas_Admin_Views_Content_Create_cshtml : System.Web.Mvc.WebViewPage<ContentPostModel>
    {
        public _Areas_Admin_Views_Content_Create_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Content\Create.cshtml"
  
  ViewBag.Title = "Create Content";
  Layout = "~/Areas/Admin/Views/Shared/_Layout_Content.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("breadcrumb", () => {

WriteLiteral("\r\n");

            
            #line 8 "..\..\Areas\Admin\Views\Content\Create.cshtml"
  
            
            #line default
            #line hidden
            
            #line 8 "..\..\Areas\Admin\Views\Content\Create.cshtml"
     Html.RenderPartial("_BreadCrumbContent", Model.ConvertToBaseModel());
            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("<div");

WriteLiteral(" class=\"card\"");

WriteLiteral(">\r\n  <div");

WriteLiteral(" class=\"card-header\"");

WriteLiteral(">\r\n    <h2>Default Basic Forms</h2>\r\n  </div>\r\n  <div");

WriteLiteral(" class=\"body\"");

WriteLiteral(">\r\n    <form");

WriteAttribute("action", Tuple.Create(" action=\"", 394), Tuple.Create("\"", 460)
            
            #line 15 "..\..\Areas\Admin\Views\Content\Create.cshtml"
, Tuple.Create(Tuple.Create("", 403), Tuple.Create<System.Object, System.Int32>(Url.Action("Create","Content",new { @area=G.AdminPath })
            
            #line default
            #line hidden
, 403), false)
);

WriteLiteral(" class=\"\"");

WriteLiteral(" method=\"post\"");

WriteLiteral(" enctype=\"multipart/form-data\"");

WriteLiteral(">\r\n");

            
            #line 16 "..\..\Areas\Admin\Views\Content\Create.cshtml"
      
            
            #line default
            #line hidden
            
            #line 16 "..\..\Areas\Admin\Views\Content\Create.cshtml"
         Html.RenderPartial("_CreateOrEdit", Model);
            
            #line default
            #line hidden
WriteLiteral("\r\n    </form>\r\n  </div>\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
