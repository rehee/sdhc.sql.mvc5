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
    
    #line 1 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    using SDHC.Common.Entity.Models;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    using SDHC.Common.Entity.Types;
    
    #line default
    #line hidden
    using WebSQL;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/AdminInputs/_AdminInput.cshtml")]
    public partial class _Areas_Admin_Views_Shared_AdminInputs__AdminInput_cshtml : System.Web.Mvc.WebViewPage<ContentPropertyIndex>
    {
        public _Areas_Admin_Views_Shared_AdminInputs__AdminInput_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
Write(Html.Hidden("Properties[" + Model.Index.ToString() + "].Key", Model.Property.Key));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 5 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
Write(Html.Hidden("Properties[" + Model.Index.ToString() + "].ValueType", Model.Property.ValueType));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 6 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
Write(Html.Hidden("Properties[" + Model.Index.ToString() + "].EditorType", Model.Property.EditorType));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 7 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
Write(Html.Hidden("Properties[" + Model.Index.ToString() + "].MultiSelect", Model.Property.MultiSelect));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 8 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
  
    var valueName = "Value";
    if (Model.Property.MultiSelect)
    {

        valueName = "MultiValue";

    }
    var inputName = "Properties[" + @Model.Index.ToString() + "]." + @valueName;
    var FileName = "Properties[" + @Model.Index.ToString() + "].File";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n<div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n    <label>");

            
            #line 22 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
      Write(Model.Property.Title);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n");

            
            #line 23 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    
            
            #line default
            #line hidden
            
            #line 23 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.Text)
    {

            
            #line default
            #line hidden
WriteLiteral("        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-control\"");

WriteAttribute("name", Tuple.Create(" name=\"", 940), Tuple.Create("\"", 957)
            
            #line 25 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 947), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 947), false)
);

WriteAttribute("value", Tuple.Create(" value=\"", 958), Tuple.Create("\"", 987)
            
            #line 25 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 966), Tuple.Create<System.Object, System.Int32>(Model.Property.Value
            
            #line default
            #line hidden
, 966), false)
);

WriteLiteral(" />\r\n");

            
            #line 26 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 27 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.Password)
    {

            
            #line default
            #line hidden
WriteLiteral("        <input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"form-control\"");

WriteAttribute("name", Tuple.Create(" name=\"", 1121), Tuple.Create("\"", 1138)
            
            #line 29 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 1128), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 1128), false)
);

WriteAttribute("value", Tuple.Create(" value=\"", 1139), Tuple.Create("\"", 1168)
            
            #line 29 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 1147), Tuple.Create<System.Object, System.Int32>(Model.Property.Value
            
            #line default
            #line hidden
, 1147), false)
);

WriteLiteral(" />\r\n");

            
            #line 30 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 31 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.TextArea)
    {

            
            #line default
            #line hidden
WriteLiteral("        <textarea");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" rows=\"5\"");

WriteAttribute("name", Tuple.Create(" name=\"", 1298), Tuple.Create("\"", 1315)
            
            #line 33 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 1305), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 1305), false)
);

WriteLiteral(">");

            
            #line 33 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                             Write(Model.Property.Value);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n");

            
            #line 34 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 35 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.DateTime)
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\'input-group date datetimepicker\'");

WriteLiteral(" id=\"mydatepicker\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\'text\'");

WriteLiteral(" class=\"form-control\"");

WriteAttribute("name", Tuple.Create(" name=\"", 1552), Tuple.Create("\"", 1569)
            
            #line 38 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 1559), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 1559), false)
);

WriteAttribute("value", Tuple.Create(" value=\"", 1570), Tuple.Create("\"", 1627)
            
            #line 38 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 1578), Tuple.Create<System.Object, System.Int32>(Model.Property.Value.Split(' ').FirstOrDefault()
            
            #line default
            #line hidden
, 1578), false)
);

WriteLiteral(" placeholder=\"yyyy-mm-dd\"");

WriteLiteral(" />\r\n            <span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" class=\"glyphicon glyphicon-calendar\"");

WriteLiteral("></span>\r\n            </span>\r\n        </div>\r\n");

            
            #line 43 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 44 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.DropDwon)
    {

            
            #line default
            #line hidden
WriteLiteral("        <select");

WriteAttribute("name", Tuple.Create(" name=\"", 1901), Tuple.Create("\"", 1918)
            
            #line 46 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 1908), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 1908), false)
);

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" ");

            
            #line 46 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                   Write(Model.Property.MultiSelect ? "multiple" : "");

            
            #line default
            #line hidden
WriteLiteral(">\r\n");

            
            #line 47 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
            
            
            #line default
            #line hidden
            
            #line 47 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
             foreach (var item in Model.Property.SelectItems)
            {
                if (item.Select)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <option selected");

WriteAttribute("value", Tuple.Create(" value=\"", 2158), Tuple.Create("\"", 2179)
            
            #line 51 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 2166), Tuple.Create<System.Object, System.Int32>(item.Value
            
            #line default
            #line hidden
, 2166), false)
);

WriteLiteral(">");

            
            #line 51 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                       Write(item.Name);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 52 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    <option");

WriteAttribute("value", Tuple.Create(" value=\"", 2291), Tuple.Create("\"", 2312)
            
            #line 55 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 2299), Tuple.Create<System.Object, System.Int32>(item.Value
            
            #line default
            #line hidden
, 2299), false)
);

WriteLiteral(">");

            
            #line 55 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                              Write(item.Name);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 56 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                }

            }

            
            #line default
            #line hidden
WriteLiteral("        </select>\r\n");

            
            #line 60 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 61 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.FileUpload)
    {

            
            #line default
            #line hidden
WriteLiteral("        <input");

WriteLiteral(" type=\"file\"");

WriteAttribute("name", Tuple.Create(" name=\"", 2497), Tuple.Create("\"", 2513)
            
            #line 63 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 2504), Tuple.Create<System.Object, System.Int32>(FileName
            
            #line default
            #line hidden
, 2504), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("        <input");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("name", Tuple.Create(" name=\"", 2547), Tuple.Create("\"", 2564)
            
            #line 64 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 2554), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 2554), false)
);

WriteAttribute("value", Tuple.Create(" value=\"", 2565), Tuple.Create("\"", 2594)
            
            #line 64 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 2573), Tuple.Create<System.Object, System.Int32>(Model.Property.Value
            
            #line default
            #line hidden
, 2573), false)
);

WriteLiteral(" />\r\n");

            
            #line 65 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591
