using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Models.ViewModels;
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
      Type type = content != null ? content.GetType().GetRealType() : BasicContentType;
      var allowChild = type.GetObjectCustomAttribute<AllowChildrenAttribute>();
      IEnumerable<string> additionalList = allowChild != null && allowChild.TableList != null ? allowChild.TableList : new string[] { "Title", "DisplayOrder" };
      var children = GetAllChildContent(parentId).OrderBy(b => b.DisplayOrder).ToList().ToList();
      var rowItems = children.Select(b =>
      {
        var values = additionalList.Select(a => b.GetPropertyByKey(a)).ToList();
        return new ContentTableRowItem(b.Id, values, b.GetType().GetRealType(), b.DisplayOrder);
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
      var homePageModel = ModelManager.Read<BaseContent>(BasicContentType, b => b.ParentId == null).OrderBy(b => b.DisplayOrder).FirstOrDefault();
      if (String.IsNullOrEmpty(url))
        goto gotoHomePage;
      var urlList = url.Split('/').Select(b => b.Trim()).Where(b => !String.IsNullOrEmpty(b)).ToList();
      var reOrgnizeUrl = String.Join("/", urlList);
      var currentUrl = urlList.LastOrDefault();
      var models = ModelManager.Read<BaseContent>(BasicContentType, b => String.Equals(b.Url, currentUrl)).ToList().Where(b => b.Parents.Count() == urlList.Count - 1).ToList();
      if (models.Count == 0)
        goto gotoHomePage;

      var model = models.Where(b => String.Equals(b.GetContentFullUrl(), reOrgnizeUrl, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
      if (model != null)
        return new ContentPostViewModel(model);
      gotoHomePage:
      if (homePageModel == null)
        return new ContentPostViewModel();
      return new ContentPostViewModel(homePageModel);
    }

    public static string GetContentFullUrl(this BaseContent model)
    {
      var parents = model.Parents.Select(b => b.Url).ToList();
      parents.Reverse();
      parents.Add(model.Url);
      return String.Join("/", parents);
    }
    public static ContentListView GetContentListView(long? id)
    {
      if (id.HasValue)
      {
        var model = GetContent(id);
        if (model == null)
        {
          return null;
        }
        return GetContentListView(model, null);
      }
      else
      {
        var roots = ModelManager.Read<BaseContent>(b => b.ParentId == null).ToList();
        var result = new ContentListView();
        roots.ForEach(b => GetContentListView(b, result));
        return result;
      }
    }

    public static ContentListView GetContentListView(BaseContent model, ContentListView parent)
    {

      var result = new ContentListView();
      result.Id = model.Id;
      result.Title = model.Title;
      result.ParentId = model.ParentId;
      result.DisplayOrder = model.DisplayOrder;

      if (parent != null)
      {
        parent.Children.Add(result);
      }
      var children = model.Children;
      foreach (var item in children)
      {
        GetContentListView(item, result);
      }
      return result;
    }

    public static long? UpdateContentOrder(IEnumerable<ContentSortPostModel> inputs)
    {
      if (inputs == null)
        return null;
      var list = inputs.ToList();
      list.RemoveAt(0);
      var idList = list.Where(b => b.id.HasValue).Select(b => b.id).ToList();
      var contents = ModelManager.Read<BaseContent>(b => idList.Contains(b.Id), out var repo).ToList();
      contents.ForEach(c =>
      {
        var cInput = list.Where(b => b.id == c.Id).FirstOrDefault();
        if (cInput != null)
        {
          if (cInput.parentId.HasValue)
          {
            if (cInput.parentId.Value > 0)
            {
              c.ParentId = cInput.parentId.Value;
            }
            else
            {
              c.ParentId = null;
            }

          }
          if (cInput.displayOrder.HasValue)
          {
            c.DisplayOrder = cInput.displayOrder.Value;
          }
        }
      });
      repo.SaveChanges();
      return null;
    }
  }

}
