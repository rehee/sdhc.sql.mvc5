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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/GeneralModelView.cshtml")]
    public partial class _Areas_Admin_Views_Shared_GeneralModelView_cshtml : System.Web.Mvc.WebViewPage<SDHC.Common.Entity.Models.ModelPostModel>
    {
        public _Areas_Admin_Views_Shared_GeneralModelView_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\Admin\Views\Shared\GeneralModelView.cshtml"
  
  var postUrl = String.IsNullOrEmpty(Model.PostUrl) ?
    Model.Id > 0 ? Url.Action("Edit", "ModelManagement", new { @area = G.AdminPath }) : Url.Action("Create", "ModelManagement", new { @area = G.AdminPath })
    : Model.PostUrl;

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"card\"");

WriteLiteral(">\r\n  <div");

WriteLiteral(" class=\"body\"");

WriteLiteral(">\r\n    <form");

WriteAttribute("action", Tuple.Create(" action=\"", 342), Tuple.Create("\"", 359)
            
            #line 9 "..\..\Areas\Admin\Views\Shared\GeneralModelView.cshtml"
, Tuple.Create(Tuple.Create("", 351), Tuple.Create<System.Object, System.Int32>(postUrl
            
            #line default
            #line hidden
, 351), false)
);

WriteLiteral(" class=\"\"");

WriteLiteral(" method=\"post\"");

WriteLiteral(" enctype=\"multipart/form-data\"");

WriteLiteral(">\r\n");

            
            #line 10 "..\..\Areas\Admin\Views\Shared\GeneralModelView.cshtml"
      
            
            #line default
            #line hidden
            
            #line 10 "..\..\Areas\Admin\Views\Shared\GeneralModelView.cshtml"
         Html.RenderPartial("_ModelFormViewCore", Model);
            
            #line default
            #line hidden
WriteLiteral("\r\n      <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-success waves-effect waves-light m-r-10\"");

WriteLiteral(">Submit</button>\r\n    </form>\r\n  </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591
