using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System
{
  public static class ContentManager
  {
    public static Type BasicContentType { get; set; } = typeof(BaseContent);
    public static string ContentViewPath { get; set; } = "";
    public static string ContentPageUrl { get; set; } = "";
    public static void CreateContent(BaseContent input, long? parentId = null)
    {
      if (parentId != null)
      {
        var parent = ContentCruds.Read<BaseContent>(parentId.Value);
        if (parent != null)
        {
          input.ParentId = parent.Id;
        }
      }

      ContentCruds.Create(input);
    }
    public static void UpdateContent(BaseContent input)
    {
      ContentCruds.Update<BaseContent>(input);
    }
    public static void UpdateContent(ContentPostModel input)
    {
      var content = ContentCruds.GetByPK<BaseContent>(input.Id, out IContent db);
      if (content == null)
      {
        return;
      }
      input.ConvertToBaseModel(content);
      db.SaveChanges();
    }
    public static void MoveContent(long contentId, long? parentId)
    {
      var content = ContentCruds.Read<BaseContent>(contentId);
      var parent = parentId.HasValue ? ContentCruds.Read<BaseContent>(parentId.Value) : null;
      if (content == null)
      {
        return;
      }
      content.ParentId = parent != null ? (long?)parent.Id : null;
      ContentCruds.Update<BaseContent>(content);
    }
    public static IEnumerable<BaseContent> GetAllChildContent(long? parentId)
    {
      return ContentCruds.Read<BaseContent>(
        b => b.ParentId == parentId)
        .AsQueryable();
    }
    public static ContentTableHtmlView GetContentTableHtmlView(long? parentId)
    {
      var content = ContentManager.GetContent(parentId);
      Type type = content != null ? content.GetType() : BasicContentType;
      var allowChild = type.GetObjectCustomAttribute<AllowChildrenAttribute>();
      IEnumerable<string> additionalList = allowChild != null && allowChild.TableList != null ? allowChild.TableList : new string[] { "Title" };
      var children = GetAllChildContent(parentId).ToList().ToList();
      var rowItems = children.Select(b =>
      {
        var values = additionalList.Select(a => b.GetPropertyByKey(a)).ToList();
        return new ContentTableRowItem(b.Id, values, b.GetType());
      }).ToList();
      var result = new ContentTableHtmlView();
      if (allowChild != null && allowChild.DisableDelete)
      {
        result.DisableDelete = true;
      }
      result.TableHeaders = additionalList.Select(b => type.GetPropertyLabelByKey(b)).ToList();
      result.Rows = rowItems;
      return result;
    }
    public static BaseContent GetContent(long? id)
    {
      if (!id.HasValue)
      {
        return null;
      }
      return ContentCruds.Read<BaseContent>(id.Value);
    }
    public static ContentPostModel GetPreCreate(long? id, string fullType)
    {
      long? parentId = null;
      if (id.HasValue)
      {
        var parent = ContentCruds.Read<BaseContent>(id.Value);
        if (parent != null)
        {
          parentId = parent.Id;
        }
      }
      var type = Type.GetType(fullType);
      if (type == null)
      {
        return null;
      }
      var model = Activator.CreateInstance(type) as BaseContent;
      model.ParentId = parentId;
      return model.ConvertModelToPost();
    }

    public static ContentPostViewModel GetContentPostViewModel(string url)
    {
      if (String.IsNullOrEmpty(url))
      {
        var model = ModelManager.Read<BaseContent>(BasicContentType, b => b.Parent == null).FirstOrDefault();
        if(model==null)
          return new ContentPostViewModel(null);
        return new ContentPostViewModel(model.ConvertModelToPost());
      }
      return new ContentPostViewModel(null);
    }


  }

}
