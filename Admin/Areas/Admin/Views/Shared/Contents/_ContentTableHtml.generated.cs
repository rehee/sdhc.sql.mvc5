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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/Contents/_ContentTableHtml.cshtml")]
    public partial class _Areas_Admin_Views_Shared_Contents__ContentTableHtml_cshtml : System.Web.Mvc.WebViewPage<ContentTableHtmlView>
    {
        public _Areas_Admin_Views_Shared_Contents__ContentTableHtml_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<table");

WriteLiteral(" class=\"table table-bordered table-striped table-hover basic-example dataTable\"");

WriteLiteral(">\r\n  <thead>\r\n    <tr>\r\n      <th>");

            
            #line 6 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
      Write(String.IsNullOrEmpty(Model.FirstRowTitle) ? "Id" : Model.FirstRowTitle);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");

            
            #line 7 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
      
            
            #line default
            #line hidden
            
            #line 7 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
       foreach (var item in Model.TableHeaders)
      {

            
            #line default
            #line hidden
WriteLiteral("        <th>");

            
            #line 9 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
       Write(item);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");

            
            #line 10 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
      }

            
            #line default
            #line hidden
WriteLiteral("      <th>\r\n        Option\r\n      </th>\r\n\r\n    </tr>\r\n  </thead>\r\n  <tfoot>\r\n    " +
"<tr>\r\n      <th>");

            
            #line 19 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
      Write(String.IsNullOrEmpty(Model.FirstRowTitle) ? "Id" : Model.FirstRowTitle);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");

            
            #line 20 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
      
            
            #line default
            #line hidden
            
            #line 20 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
       foreach (var item in Model.TableHeaders)
      {

            
            #line default
            #line hidden
WriteLiteral("        <th>");

            
            #line 22 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
       Write(item);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");

            
            #line 23 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
      }

            
            #line default
            #line hidden
WriteLiteral("      <th>\r\n        Option\r\n      </th>\r\n    </tr>\r\n  </tfoot>\r\n  <tbody>\r\n");

            
            #line 30 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
    
            
            #line default
            #line hidden
            
            #line 30 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
     foreach (var row in Model.Rows)
    {

            
            #line default
            #line hidden
WriteLiteral("      <tr>\r\n        <td>\r\n");

            
            #line 34 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
          
            
            #line default
            #line hidden
            
            #line 34 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
           if (!Model.FirstRowIsNotAction)
          {
            if (Model.FirstRow == null)
            {

            
            #line default
            #line hidden
WriteLiteral("              <a");

WriteAttribute("href", Tuple.Create(" href=\"", 864), Tuple.Create("\"", 955)
            
            #line 38 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
, Tuple.Create(Tuple.Create("", 871), Tuple.Create<System.Object, System.Int32>(Url.Action(Model.FirstRowAction,Model.FirstRowController,Model.FirstRowObject(row))
            
            #line default
            #line hidden
, 871), false)
);

WriteLiteral(">");

            
            #line 38 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
                                                                                                        Write(row.Id);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 39 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("              <a");

WriteAttribute("class", Tuple.Create(" class=\"", 1034), Tuple.Create("\"", 1069)
            
            #line 42 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
, Tuple.Create(Tuple.Create("", 1042), Tuple.Create<System.Object, System.Int32>(Model.FirstRow.ButtonClass
            
            #line default
            #line hidden
, 1042), false)
);

WriteAttribute("href", Tuple.Create(" href=\"", 1070), Tuple.Create("\"", 1173)
            
            #line 42 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
, Tuple.Create(Tuple.Create("", 1077), Tuple.Create<System.Object, System.Int32>(Url.Action(Model.FirstRow.RowAction,Model.FirstRow.RowController,Model.FirstRow.RowObject(row))
            
            #line default
            #line hidden
, 1077), false)
);

WriteLiteral(" ");

            
            #line 42 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
                                                                                                                                                        Write(Model.FirstRow.UrlAttribute(row));

            
            #line default
            #line hidden
WriteLiteral(">");

            
            #line 42 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
                                                                                                                                                                                          Write(Model.FirstRow.RowTitle(row));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 43 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
            }
          }
          else
          {
            if (Model.FirstRow == null)
            {

            
            #line default
            #line hidden
WriteLiteral("              <span>");

            
            #line 49 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
               Write(row.Id);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");

            
            #line 50 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("              <span>");

            
            #line 53 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
               Write(Model.FirstRow.RowTitle(row));

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");

            
            #line 54 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
            }
          }

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n");

            
            #line 58 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
        
            
            #line default
            #line hidden
            
            #line 58 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
         foreach (var item in row.Values)
        {

            
            #line default
            #line hidden
WriteLiteral("          <td>");

            
            #line 60 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
         Write(item);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 61 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("          <td>\r\n");

            
            #line 63 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
            
            
            #line default
            #line hidden
            
            #line 63 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
             if (Model.Options != null)
            {
              foreach (var option in Model.Options)
              {

            
            #line default
            #line hidden
WriteLiteral("                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1795), Tuple.Create("\"", 1875)
            
            #line 67 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
, Tuple.Create(Tuple.Create("", 1802), Tuple.Create<System.Object, System.Int32>(Url.Action(option.RowAction,option.RowController, option.RowObject(row))
            
            #line default
            #line hidden
, 1802), false)
);

WriteLiteral(" ");

            
            #line 67 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
                                                                                               Write(option.UrlAttribute(row));

            
            #line default
            #line hidden
WriteLiteral(" class=\"");

            
            #line 67 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
                                                                                                                                Write(option.ButtonClass);

            
            #line default
            #line hidden
WriteLiteral("\">");

            
            #line 67 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
                                                                                                                                                     Write(option.RowTitle(row));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 68 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
              }
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 71 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
            
            
            #line default
            #line hidden
            
            #line 71 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
             if (!String.IsNullOrEmpty(Model.DeleteFunctionName) && !Model.DisableDelete)
            {

            
            #line default
            #line hidden
WriteLiteral("              <button");

WriteLiteral(" class=\"btn btn-danger\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 2142), Tuple.Create("\"", 2196)
            
            #line 73 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
, Tuple.Create(Tuple.Create("", 2152), Tuple.Create<System.Object, System.Int32>(Model.DeleteFunctionName+"("+ row.Id +")"
            
            #line default
            #line hidden
, 2152), false)
);

WriteLiteral(">Delete</button>\r\n");

            
            #line 74 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("          </td>\r\n\r\n      </tr>\r\n");

            
            #line 78 "..\..\Areas\Admin\Views\Shared\Contents\_ContentTableHtml.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("  </tbody>\r\n</table>\r\n");

        }
    }
}
#pragma warning restore 1591
