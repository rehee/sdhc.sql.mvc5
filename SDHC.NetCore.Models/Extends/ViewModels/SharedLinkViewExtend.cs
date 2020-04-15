using SDHC.Common.Entity.Models;
using SDHC.NetCore.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
  public static class SharedLinkViewExtend
  {
    public static SharedLinkView GetSharedLinkView<T>(this IPostModeltViewModal<T> model, string key) where T : IPostModel
    {
      var lang = model.Lang;
      return new SharedLinkView(model.GetContentPropertyByName(key).Property, lang);
    }
    public static SharedLinkView GetSharedLinkView(this ContentPropertyIndex model)
    {
      return new SharedLinkView(model.Property, model.Lang);
    }
    public static SharedLinkView GetSharedLinkView(this ContentProperty model)
    {
      return new SharedLinkView(model);
    }

    public static SharedLinkPost GetSharedLinkPost<T>(this IPostModeltViewModal<T> model, string key) where T : IPostModel
    {
      return model.GetContentPropertyByName(key).Property.GetSharedLinkPost(model.Lang);
    }
    public static SharedLinkPost GetSharedLinkPost(this ContentPropertyIndex model, int? lang = null)
    {
      return model.Property.GetSharedLinkPost(lang.HasValue ? lang : model.Lang);
    }
    public static SharedLinkPost GetSharedLinkPost(this ContentProperty model, int? lang = null)
    {

      var type = model.RelatedType;
      var allowChild = type.GetCustomAttribute<AllowChildrenAttribute>();
      var header = allowChild?.TableList ?? Enumerable.Empty<string>();
      var image = allowChild?.TableList ?? Enumerable.Empty<string>();
      var models = ServiceContainer.ModelService.Read<ISharedLink>(type, b => lang.HasValue ? b.Lang == lang : true).OrderBy(b => b.DisplayOrder);
      var result = new SharedLinkPost
      {
        Headers = header,
        Images = image,
        Models = models,
        Lang = lang,
        TypeName = type?.FullName,
        AssemblyName = type?.Assembly.FullName,
        View = new SharedLinkView(Newtonsoft.Json.JsonConvert.SerializeObject(models), model.Key)
      };

      return result;
    }



  }
}
