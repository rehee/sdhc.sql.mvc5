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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/ModelManagement/_CreateOrEdit.cshtml")]
    public partial class _Areas_Admin_Views_ModelManagement__CreateOrEdit_cshtml : System.Web.Mvc.WebViewPage<SDHC.Common.Entity.Models.ModelPostModel>
    {
        public _Areas_Admin_Views_ModelManagement__CreateOrEdit_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\Admin\Views\ModelManagement\_CreateOrEdit.cshtml"
Write(Html.Hidden("Id", Model.Id));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 3 "..\..\Areas\Admin\Views\ModelManagement\_CreateOrEdit.cshtml"
Write(Html.Hidden("FullType", Model.FullType));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 4 "..\..\Areas\Admin\Views\ModelManagement\_CreateOrEdit.cshtml"
Write(Html.Hidden("ThisAssembly", Model.ThisAssembly));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 5 "..\..\Areas\Admin\Views\ModelManagement\_CreateOrEdit.cshtml"
   Html.RenderPartial("AdminInputs/_PropertiesEdit", Model);
            
            #line default
            #line hidden
WriteLiteral("\r\n<button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-success waves-effect waves-light m-r-10\"");

WriteLiteral(">Submit</button>\r\n");

        }
    }
}
#pragma warning restore 1591