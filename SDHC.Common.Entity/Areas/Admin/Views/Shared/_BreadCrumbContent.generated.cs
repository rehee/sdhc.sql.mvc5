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
    using SDHC.Common.Entity;
    
    #line 1 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
    using SDHC.Common.Entity.Models;
    
    #line default
    #line hidden
    using WebSQL;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/_BreadCrumbContent.cshtml")]
    public partial class _Areas_Admin_Views_Shared__BreadCrumbContent_cshtml : System.Web.Mvc.WebViewPage<BaseContent>
    {
        public _Areas_Admin_Views_Shared__BreadCrumbContent_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
  
    var breadcrumb = new List<BaseContent>();
    if (Model != null)
    {
        var parents = Model.Parents.Select(b => b).ToList();
        parents.Reverse();
        parents.ForEach(b => breadcrumb.Add(b));
        breadcrumb.Add(Model);
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n<ol");

WriteLiteral(" class=\"breadcrumb\"");

WriteLiteral(">\r\n    <li");

WriteAttribute("class", Tuple.Create(" class=\"", 350), Tuple.Create("\"", 408)
, Tuple.Create(Tuple.Create("", 358), Tuple.Create("breadcrumb-item", 358), true)
            
            #line 14 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
, Tuple.Create(Tuple.Create(" ", 373), Tuple.Create<System.Object, System.Int32>(breadcrumb.Count==0?"active":""
            
            #line default
            #line hidden
, 374), false)
);

WriteLiteral(">\r\n        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 422), Tuple.Create("\"", 486)
            
            #line 15 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
, Tuple.Create(Tuple.Create("", 429), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Content",new { area="admin",id="" })
            
            #line default
            #line hidden
, 429), false)
);

WriteLiteral(">Root</a>\r\n    </li>\r\n");

            
            #line 17 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
    
            
            #line default
            #line hidden
            
            #line 17 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
     foreach (var item in breadcrumb)
    {

            
            #line default
            #line hidden
WriteLiteral("        <li");

WriteAttribute("class", Tuple.Create(" class=\"", 566), Tuple.Create("\"", 649)
, Tuple.Create(Tuple.Create("", 574), Tuple.Create("breadcrumb-item", 574), true)
            
            #line 19 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
, Tuple.Create(Tuple.Create(" ", 589), Tuple.Create<System.Object, System.Int32>(breadcrumb.IndexOf(item)==breadcrumb.Count-1?"active":""
            
            #line default
            #line hidden
, 590), false)
);

WriteLiteral(">\r\n            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 667), Tuple.Create("\"", 739)
            
            #line 20 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
, Tuple.Create(Tuple.Create("", 674), Tuple.Create<System.Object, System.Int32>(Url.Action("Index","Content",new { area="admin",@id = item.Id })
            
            #line default
            #line hidden
, 674), false)
);

WriteLiteral(">");

            
            #line 20 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
                                                                                   Write(item.GetType().Name);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n        </li>\r\n");

            
            #line 22 "..\..\Areas\Admin\Views\Shared\_BreadCrumbContent.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</ol>\r\n");

        }
    }
}
#pragma warning restore 1591
