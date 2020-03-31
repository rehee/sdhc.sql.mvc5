using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.Common.Entity.Models
{
  public class ContentPostViewModel
  {
    public static Func<string> GetContentViewPath { get; set; } = () => ConfigContainer.Systems.ContentViewPath;
    public static Func<string> GetContentPageUrl { get; set; } = () => ConfigContainer.Systems.ContentPageUrl;
    public static Func<IContentModel, ContentPostModel> Convert { get; set; } = (input) => input.ConvertModelToPost();

    public ContentPostViewModel(ContentPostModel model = null)
    {
      if (model != null)
        Model = model;
    }
    public ContentPostViewModel(IContentModel model)
    {
      if (model != null)
      {
        Model = Convert(model);
        Parents = model.Parents;
        ThisUrl = model.Url;
      }


    }

    public ContentPostModel Model { get; set; }
    public string ViewPath
    {
      get
      {
        if (Model == null)
          return "";
        var t = Type.GetType($"{Model.FullType},{Model.ThisAssembly}");
        var path = String.IsNullOrEmpty(GetContentViewPath()) ? "" : $"/{GetContentViewPath()}";
        return $"~/Views{path}/{t.Name}.cshtml";
      }
    }
    IEnumerable<IContentModel> Parents { get; set; } = Enumerable.Empty<IContentModel>();
    IEnumerable<IContentModel> BreadCrumbs
    {
      get
      {
        if (Parents == null)
          Enumerable.Empty<IContentModel>();
        var list = Parents.ToList();
        list.Reverse();
        return list;
      }
    }
    public string ThisUrl { get; set; }
    public string Url
    {
      get
      {
        return $"/{GetContentPageUrl()}{(BreadCrumbs.Count() == 0 ? "" : "/")}{String.Join("/", BreadCrumbs.Select(b => b.Url))}/{ThisUrl}";
      }
    }
  }


}
