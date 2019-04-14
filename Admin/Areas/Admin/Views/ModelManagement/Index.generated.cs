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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/ModelManagement/Index.cshtml")]
    public partial class _Areas_Admin_Views_ModelManagement_Index_cshtml : System.Web.Mvc.WebViewPage<ContentTableHtmlView>
    {
        public _Areas_Admin_Views_ModelManagement_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Areas\Admin\Views\ModelManagement\Index.cshtml"
  
  ViewBag.Title = G.GetModelTitle(C.Text(ViewBag.id)) + " Management";
  Model.FirstRowAction = "Edit";
  Model.FirstRowController = "ModelManagement";
  Model.FirstRowArea = "Area";
  Model.FirstRowObject = b => new { @area = G.AdminPath, @id = (b as ContentTableRowItem).Id, @type = ModelManager.GetMapperKey((b as ContentTableRowItem).ThisType.FullName) };
  Model.DeleteFunctionName = "deleteRole";

            
            #line default
            #line hidden
WriteLiteral("\r\n<form");

WriteLiteral(" id=\"modelDeleteForm\"");

WriteAttribute("action", Tuple.Create(" action=\"", 471), Tuple.Create("\"", 539)
            
            #line 10 "..\..\Areas\Admin\Views\ModelManagement\Index.cshtml"
, Tuple.Create(Tuple.Create("", 480), Tuple.Create<System.Object, System.Int32>(Url.Action("Delete","ModelManagement","@area=G.AdminPath")
            
            #line default
            #line hidden
, 480), false)
);

WriteLiteral(" method=\"post\"");

WriteLiteral(">\r\n  <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"type\"");

WriteAttribute("value", Tuple.Create(" value=\"", 591), Tuple.Create("\"", 610)
            
            #line 11 "..\..\Areas\Admin\Views\ModelManagement\Index.cshtml"
, Tuple.Create(Tuple.Create("", 599), Tuple.Create<System.Object, System.Int32>(ViewBag.id
            
            #line default
            #line hidden
, 599), false)
);

WriteLiteral(" />\r\n  <input");

WriteLiteral(" id=\"deleteId\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"deleteId\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" />\r\n</form>\r\n<div");

WriteLiteral(" class=\"row clearfix\"");

WriteLiteral(">\r\n  <div");

WriteLiteral(" class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"card\"");

WriteLiteral(">\r\n      <div");

WriteLiteral(" class=\"card-header\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"button-box\"");

WriteLiteral(">\r\n\r\n          <a");

WriteAttribute("href", Tuple.Create(" href=\"", 881), Tuple.Create("\"", 968)
            
            #line 20 "..\..\Areas\Admin\Views\ModelManagement\Index.cshtml"
, Tuple.Create(Tuple.Create("", 888), Tuple.Create<System.Object, System.Int32>(Url.Action("Create","ModelManagement",new { @area=G.AdminPath,@id=ViewBag.id })
            
            #line default
            #line hidden
, 888), false)
);

WriteLiteral(" class=\"btn btn-info btn-group\"");

WriteLiteral(">Create</a>\r\n        </div>\r\n\r\n      </div>\r\n\r\n      <div");

WriteLiteral(" class=\"body\"");

WriteLiteral(">\r\n");

            
            #line 26 "..\..\Areas\Admin\Views\ModelManagement\Index.cshtml"
        
            
            #line default
            #line hidden
            
            #line 26 "..\..\Areas\Admin\Views\ModelManagement\Index.cshtml"
           Html.RenderPartial("Contents/_ContentTableHtml", Model); 
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n");

DefineSection("script", () => {

WriteLiteral("\r\n  <script>\r\n    $(function () {\r\n      $(\'.basic-example\').DataTable();\r\n    })" +
";\r\n  </script>\r\n  ");

WriteLiteral(@"
  <script>
    $(function () {
      $('.sweetalert_delete.delete_button').on('click', function () {
        var id = $(this).data('id');
        showCancelMessage(function(){
          $('#deleteId').val(id);
          if(id){
            document.getElementById('modelDeleteForm').submit()
          }
        })
      });
    });

   function deleteRole(id){
     showCancelMessage(function(){
          $('#deleteId').val(id);
          if(id){
            document.getElementById('modelDeleteForm').submit()
          }
        })
   }

   function showCancelMessage(callback) {
      swal({
        title: ""Are you sure?"",
        text: ""You will not be able to recover!"",
        type: ""warning"",
        showCancelButton: true,
        confirmButtonColor: ""#DD6B55"",
        confirmButtonText: ""Yes, delete it!"",
        cancelButtonText: ""No, cancel plx!"",
        closeOnConfirm: false,
        closeOnCancel: false
      }, function (isConfirm) {
        if (isConfirm) {
          swal(""Deleted!"", ""Your record has been deleted."", ""success"");
          if(callback){
            callback();
          }
        } else {
          swal(""Cancelled"", ""Your record is safe :)"", ""error"");
        }
      });
    }

    
  </script>
");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
