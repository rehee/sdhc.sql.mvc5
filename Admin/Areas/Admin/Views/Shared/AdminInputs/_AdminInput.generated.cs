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

#line 20 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
public System.Web.WebPages.HelperResult requiredMark(bool required)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 21 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
 
    if (required)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        ");

WriteLiteralTo(__razor_helper_writer, "*");

WriteLiteralTo(__razor_helper_writer, "\r\n");


#line 25 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }


#line default
#line hidden
});

#line 26 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
}
#line default
#line hidden

#line 27 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
public System.Web.WebPages.HelperResult requiredAttr(bool required)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 28 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
 
    if (required)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        ");

WriteLiteralTo(__razor_helper_writer, "required");

WriteLiteralTo(__razor_helper_writer, "\r\n");


#line 32 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }


#line default
#line hidden
});

#line 33 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
}
#line default
#line hidden

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
WriteLiteral("\r\n\r\n");

            
            #line 34 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
 if (Model.Property.NewLine)
{

            
            #line default
            #line hidden
WriteLiteral("    <br />\r\n");

            
            #line 37 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("<div");

WriteLiteral(" class=\"form-group\"");

WriteAttribute("style", Tuple.Create(" style=\"", 1035), Tuple.Create("\"", 1137)
            
            #line 38 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 1043), Tuple.Create<System.Object, System.Int32>(Model.Property.InputWidth>0?"width:"+Model.Property.InputWidth+"%;display:inline-block;":""
            
            #line default
            #line hidden
, 1043), false)
);

WriteLiteral(">\r\n    <label>");

            
            #line 39 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
      Write(Model.Property.Title);

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 39 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                            Write(requiredMark(Model.Property.Required));

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n");

            
            #line 40 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    
            
            #line default
            #line hidden
            
            #line 40 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.Text)
    {

            
            #line default
            #line hidden
WriteLiteral("        <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" ");

            
            #line 42 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                      Write(requiredAttr(Model.Property.Required));

            
            #line default
            #line hidden
WriteLiteral(" class=\"form-control\" name=\"");

            
            #line 42 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                        Write(inputName);

            
            #line default
            #line hidden
WriteLiteral("\" value=\"");

            
            #line 42 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                                           Write(Model.Property.Value);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n");

            
            #line 43 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 44 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.Password)
    {

            
            #line default
            #line hidden
WriteLiteral("        <input");

WriteLiteral(" type=\"password\"");

WriteLiteral(" ");

            
            #line 46 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                          Write(requiredAttr(Model.Property.Required));

            
            #line default
            #line hidden
WriteLiteral(" class=\"form-control\" name=\"");

            
            #line 46 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                            Write(inputName);

            
            #line default
            #line hidden
WriteLiteral("\" value=\"");

            
            #line 46 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                                               Write(Model.Property.Value);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n");

            
            #line 47 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 48 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.TextArea)
    {

            
            #line default
            #line hidden
WriteLiteral("        <textarea");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" ");

            
            #line 50 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                  Write(requiredAttr(Model.Property.Required));

            
            #line default
            #line hidden
WriteLiteral(" rows=\"5\" name=\"");

            
            #line 50 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                        Write(inputName);

            
            #line default
            #line hidden
WriteLiteral("\">");

            
            #line 50 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                                    Write(Model.Property.Value);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n");

            
            #line 51 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 52 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.DateTime)
    {
        
            
            #line default
            #line hidden
            
            #line 59 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                    

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"input-group date form_datetime \"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"input-group-addon bg\"");

WriteLiteral("><i");

WriteLiteral(" class=\"fa fa-calendar\"");

WriteLiteral("></i></span>\r\n            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" ");

            
            #line 62 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                          Write(requiredAttr(Model.Property.Required));

            
            #line default
            #line hidden
WriteLiteral(" name=\"");

            
            #line 62 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                       Write(inputName);

            
            #line default
            #line hidden
WriteLiteral("\" value=\"");

            
            #line 62 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                           Write(!String.IsNullOrEmpty(Model.Property.Value)?Model.Property.Value.Contains("T")? Model.Property.Value.Split('T').FirstOrDefault() : Model.Property.Value.Split(' ').FirstOrDefault() :"" );

            
            #line default
            #line hidden
WriteLiteral("\" placeholder=\"yyyy-mm-dd\" readonly class=\"form-control\">\r\n\r\n        </div>\r\n");

            
            #line 65 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 66 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.DropDwon)
    {

            
            #line default
            #line hidden
WriteLiteral("        <select");

WriteAttribute("name", Tuple.Create(" name=\"", 2937), Tuple.Create("\"", 2954)
            
            #line 68 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 2944), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 2944), false)
);

WriteLiteral(" ");

            
            #line 68 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                             Write(requiredAttr(Model.Property.Required));

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 68 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                     Write(Model.Property.Readonly ? "disabled" : "");

            
            #line default
            #line hidden
WriteLiteral(" class=\"form-control\" ");

            
            #line 68 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                                                                       Write(Model.Property.MultiSelect ? "multiple" : "");

            
            #line default
            #line hidden
WriteLiteral(">\r\n");

            
            #line 69 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
            
            
            #line default
            #line hidden
            
            #line 69 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
             foreach (var item in Model.Property.SelectItems)
            {
                if (item.Select)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <option selected");

WriteAttribute("value", Tuple.Create(" value=\"", 3278), Tuple.Create("\"", 3299)
            
            #line 73 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 3286), Tuple.Create<System.Object, System.Int32>(item.Value
            
            #line default
            #line hidden
, 3286), false)
);

WriteLiteral(">");

            
            #line 73 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                       Write(item.Name);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 74 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    <option");

WriteAttribute("value", Tuple.Create(" value=\"", 3411), Tuple.Create("\"", 3432)
            
            #line 77 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 3419), Tuple.Create<System.Object, System.Int32>(item.Value
            
            #line default
            #line hidden
, 3419), false)
);

WriteLiteral(">");

            
            #line 77 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                              Write(item.Name);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 78 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                }

            }

            
            #line default
            #line hidden
WriteLiteral("        </select>\r\n");

            
            #line 82 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 83 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
     if (Model.Property.EditorType == EnumInputType.FileUpload)
    {
        if (!string.IsNullOrEmpty(Model.Property.Value))
        {

            
            #line default
            #line hidden
WriteLiteral("            <p>\r\n                <img");

WriteLiteral(" style=\"display:block; max-width: 120px;max-height: 120px; margin-bottom: 10px;\"");

WriteAttribute("src", Tuple.Create(" src=\"", 3777), Tuple.Create("\"", 3817)
, Tuple.Create(Tuple.Create("", 3783), Tuple.Create("/", 3783), true)
            
            #line 88 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                           , Tuple.Create(Tuple.Create("", 3784), Tuple.Create<System.Object, System.Int32>(Model.Property.Value.ImagePath()
            
            #line default
            #line hidden
, 3784), false)
);

WriteLiteral(" />\r\n            </p>\r\n");

            
            #line 90 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"

        }

            
            #line default
            #line hidden
WriteLiteral("        <input");

WriteLiteral(" style=\"display:block\"");

WriteLiteral(" type=\"file\"");

WriteLiteral(" ");

            
            #line 92 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                            Write(requiredAttr(Model.Property.Required));

            
            #line default
            #line hidden
WriteLiteral(" class=\"form-control\" name=\"");

            
            #line 92 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
                                                                                                              Write(FileName);

            
            #line default
            #line hidden
WriteLiteral("\" /> ");

WriteLiteral("<br />\r\n");

WriteLiteral("        <input");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("name", Tuple.Create(" name=\"", 4019), Tuple.Create("\"", 4036)
            
            #line 93 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 4026), Tuple.Create<System.Object, System.Int32>(inputName
            
            #line default
            #line hidden
, 4026), false)
);

WriteAttribute("value", Tuple.Create(" value=\"", 4037), Tuple.Create("\"", 4066)
            
            #line 93 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
, Tuple.Create(Tuple.Create("", 4045), Tuple.Create<System.Object, System.Int32>(Model.Property.Value
            
            #line default
            #line hidden
, 4045), false)
);

WriteLiteral(" />\r\n");

            
            #line 94 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 96 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
 if (Model.Property.NewLineAfter)
{

            
            #line default
            #line hidden
WriteLiteral("    <br />\r\n");

            
            #line 99 "..\..\Areas\Admin\Views\Shared\AdminInputs\_AdminInput.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
