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
    
    #line 1 "..\..\Areas\Admin\Views\Content\Index.cshtml"
    using SDHC.Common.Entity.Models;
    
    #line default
    #line hidden
    using WebSQL;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Content/Index.cshtml")]
    public partial class _Areas_Admin_Views_Content_Index_cshtml : System.Web.Mvc.WebViewPage<BaseContent>
    {
        public _Areas_Admin_Views_Content_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Content\Index.cshtml"
  
    ViewBag.Title = "Content";
    Layout = "~/Areas/Admin/Views/Shared/_Layout_Content.cshtml";
    var contentId = Model != null ? (long?)Model.Id : null;
    var contents = ContentManager.GetContentTableHtmlView(contentId);
    contents.FirstRowAction = "Index";
    contents.FirstRowArea = G.AdminPath;
    contents.FirstRowController = "Content";
    contents.FirstRowObject = b => new { @id = (b as ContentTableRowItem).Id, @area = G.AdminPath };
    IEnumerable<string> CreateRole;
    IEnumerable<string> ReadRole;
    IEnumerable<string> UpdateRole;
    IEnumerable<string> DeleteRole;
    if (Model == null)
    {
        CreateRole = Enumerable.Empty<string>();
        ReadRole = Enumerable.Empty<string>();
        UpdateRole = Enumerable.Empty<string>();
        DeleteRole = Enumerable.Empty<string>();
        //var type = ContentE.RootType.CustomAttributes.Where(b => b.AttributeType == typeof(SDHCC.AllowChildrenAttribute)).FirstOrDefault();
        //if (type != null)
        //{
        //    var createRoles = type.NamedArguments.Where(b => b.MemberName == "CreateRoles").FirstOrDefault();
        //    if (createRoles != null)
        //    {
        //        var role = (System.Collections.ObjectModel.ReadOnlyCollection<System.Reflection.CustomAttributeTypedArgument>)createRoles.TypedValue.Value;
        //        CreateRole = role.Select(b => b.Value.ToString()).ToList();
        //    }
        //    var editRoles = type.NamedArguments.Where(b => b.MemberName == "EditRoles").FirstOrDefault();
        //    if (editRoles != null)
        //    {
        //        var eRoles = (System.Collections.ObjectModel.ReadOnlyCollection<System.Reflection.CustomAttributeTypedArgument>)editRoles.TypedValue.Value;
        //        UpdateRole = eRoles.Select(b => b.Value.ToString()).ToList();
        //    }
        //}
    }
    else
    {
        //CreateRole = Model.AdminCreateRoles;
        //ReadRole = Model.AdminReadRoles;
        //UpdateRole = Model.AdminUpdateRoles;
        //DeleteRole = Model.AdminDeleteRoles;
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

DefineSection("breadcrumb", () => {

WriteLiteral("\r\n");

            
            #line 49 "..\..\Areas\Admin\Views\Content\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 49 "..\..\Areas\Admin\Views\Content\Index.cshtml"
       Html.RenderPartial("_BreadCrumbContent", Model);
            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("<div");

WriteLiteral(" class=\"row clearfix\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"card\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"card-header\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"button-box\"");

WriteLiteral(">\r\n\r\n                    <div");

WriteLiteral(" class=\"btn-group\"");

WriteLiteral(">\r\n                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-info btn-group\"");

WriteLiteral(" data-toggle=\"dropdown\"");

WriteLiteral(" aria-haspopup=\"true\"");

WriteLiteral(" aria-expanded=\"false\"");

WriteLiteral(">\r\n                            Create\r\n                        </button>\r\n       " +
"                 <ul");

WriteLiteral(" class=\"dropdown-menu\"");

WriteLiteral(">\r\n");

            
            #line 62 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 62 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                              
                                var avaliableChild = TypeExtends.GetAllowChildrens(Model);
                            
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 65 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 65 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                             foreach (var child in avaliableChild)
                            {
                                var idguid = Guid.NewGuid().ToString();

            
            #line default
            #line hidden
WriteLiteral("                                <li>\r\n                                    <a");

WriteLiteral(" href=\"#\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 3168), Tuple.Create("\"", 3197)
, Tuple.Create(Tuple.Create("", 3178), Tuple.Create("subbform(\'", 3178), true)
            
            #line 69 "..\..\Areas\Admin\Views\Content\Index.cshtml"
, Tuple.Create(Tuple.Create("", 3188), Tuple.Create<System.Object, System.Int32>(idguid
            
            #line default
            #line hidden
, 3188), false)
, Tuple.Create(Tuple.Create("", 3195), Tuple.Create("\')", 3195), true)
);

WriteLiteral(">");

            
            #line 69 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                                                                         Write(TypeExtends.GetClassDisplayName(child));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                                    <form");

WriteAttribute("id", Tuple.Create(" id=\"", 3285), Tuple.Create("\"", 3297)
            
            #line 70 "..\..\Areas\Admin\Views\Content\Index.cshtml"
, Tuple.Create(Tuple.Create("", 3290), Tuple.Create<System.Object, System.Int32>(idguid
            
            #line default
            #line hidden
, 3290), false)
);

WriteAttribute("action", Tuple.Create(" action=\"", 3298), Tuple.Create("\"", 3371)
            
            #line 70 "..\..\Areas\Admin\Views\Content\Index.cshtml"
, Tuple.Create(Tuple.Create("", 3307), Tuple.Create<System.Object, System.Int32>(Url.Action("PreCreate", "Content", new { @area = G.AdminPath })
            
            #line default
            #line hidden
, 3307), false)
);

WriteLiteral(" method=\"post\"");

WriteLiteral(">\r\n");

WriteLiteral("                                        ");

            
            #line 71 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                                   Write(Html.Hidden("ContentId", contentId));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                                        ");

            
            #line 72 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                                   Write(Html.Hidden("FullType", child.FullName + "," + child.Assembly.FullName));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                    </form>\r\n                                </" +
"li>\r\n");

            
            #line 75 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                            }

            
            #line default
            #line hidden
WriteLiteral(@"                        </ul>
                    </div>
                    <script>
                        function subbform(id) {
                            var form = document.getElementById(id);
                            form.submit();
                            return;
                        }
                    </script>
                    <a");

WriteAttribute("href", Tuple.Create(" href=\"", 4064), Tuple.Create("\"", 4147)
            
            #line 85 "..\..\Areas\Admin\Views\Content\Index.cshtml"
, Tuple.Create(Tuple.Create("", 4071), Tuple.Create<System.Object, System.Int32>(Url.Action("Edit", "Content", new { @area = G.AdminPath, @id = contentId })
            
            #line default
            #line hidden
, 4071), false)
);

WriteLiteral(" class=\"btn btn-success\"");

WriteLiteral(">Edit</a>\r\n                    <a");

WriteAttribute("href", Tuple.Create(" href=\"", 4205), Tuple.Create("\"", 4288)
            
            #line 86 "..\..\Areas\Admin\Views\Content\Index.cshtml"
, Tuple.Create(Tuple.Create("", 4212), Tuple.Create<System.Object, System.Int32>(Url.Action("Sort", "Content", new { @area = G.AdminPath, @id = contentId })
            
            #line default
            #line hidden
, 4212), false)
);

WriteLiteral(" class=\"btn btn-warning\"");

WriteLiteral(">Sort</a>\r\n                    <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" onclick=\"deleteCurrent()\"");

WriteLiteral(" class=\"btn btn-danger\"");

WriteLiteral(">Delete</a>\r\n                    <form");

WriteLiteral(" id=\"ContentDeleteForm\"");

WriteAttribute("action", Tuple.Create(" action=\"", 4465), Tuple.Create("\"", 4535)
            
            #line 88 "..\..\Areas\Admin\Views\Content\Index.cshtml"
, Tuple.Create(Tuple.Create("", 4474), Tuple.Create<System.Object, System.Int32>(Url.Action("Delete", "Content", new { @area = G.AdminPath })
            
            #line default
            #line hidden
, 4474), false)
);

WriteLiteral(" method=\"post\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 89 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                   Write(Html.Hidden("id", contentId));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </form>\r\n                </div>\r\n\r\n            </div>\r\n\r\n  " +
"          <div");

WriteLiteral(" class=\"body\"");

WriteLiteral(">\r\n");

            
            #line 96 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                
            
            #line default
            #line hidden
            
            #line 96 "..\..\Areas\Admin\Views\Content\Index.cshtml"
                   Html.RenderPartial("Contents/_ContentTableHtml", contents); 
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");

DefineSection("script", () => {

WriteLiteral("\r\n    <script>\r\n        $(function () {\r\n            $(\'.basic-example\').DataTabl" +
"e();\r\n        });\r\n    </script>\r\n    ");

WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
