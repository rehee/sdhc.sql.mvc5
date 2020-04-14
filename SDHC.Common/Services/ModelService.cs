using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SDHC.Common.Services
{
  public class ModelService : CrudModel, IModelService
  {
    public ModelService(ICrudInit container) : base(container)
    { }
    public Dictionary<string, Type> ModelMapper { get; set; } = new Dictionary<string, Type>();
    private Dictionary<string, Type> SharedContentMapper { get; } = new Dictionary<string, Type>();

    public void AddSharedContent<T>(string key) where T : ISharedContent, new()
    {
      if (string.IsNullOrWhiteSpace(key))
        return;
      key = key.Trim();
      if (SharedContentMapper.ContainsKey(key))
      {
        SharedContentMapper[key] = typeof(T);
      }
      else
      {
        SharedContentMapper.Add(key, typeof(T));
      }
    }
    public ISharedContent GetSharedContent(string key, int lang, ISave repo = null)
    {
      if (repo == null)
        repo = GetRepo();
      if (!SharedContentMapper.TryGetValue(key, out var type))
        return null;
      var model = Read<ISharedContent>(type, b => b.Lang == lang, repo).FirstOrDefault();
      if (model == null)
      {
        model = Activator.CreateInstance(type) as ISharedContent;
        model.Lang = lang;
        Create(model, repo);
      }
      return model;
    }
    public IDictionary<string, ISharedContent> getAllSharedContentByLang(int lang)
    {
      var repo = GetRepo();
      var result = new Dictionary<string, ISharedContent>();
      foreach (var m in SharedContentMapper)
      {
        result.Add(m.Key, GetSharedContent(m.Key, lang, repo));
      }
      return result;
    }
    public IDictionary<Tuple<string, int>, ISharedContent> getAllSharedContentByLangs(IEnumerable<int> lang)
    {
      var repo = GetRepo();
      var result = new Dictionary<Tuple<string, int>, ISharedContent>();
      foreach (var m in SharedContentMapper)
      {
        foreach (var l in lang)
        {
          result.Add(new Tuple<string, int>(m.Key, l), GetSharedContent(m.Key, l, repo));
        }
      }
      return result;
    }

    public IEnumerable<string> ModelManagerMapper
    {
      get
      {
        return ModelMapper.Select(b => b.Key);
      }
    }
    public Type GetModelType(string typeKey)
    {
      if (string.IsNullOrEmpty(typeKey))
      {
        return null;
      }
      if (ModelMapper == null || !ModelMapper.ContainsKey(typeKey))
      {
        return null;
      }
      return ModelMapper[typeKey];
    }
    public string GetMapperKey(string type)
    {
      if (ModelMapper == null)
      {
        return null;
      }
      var key = ModelMapper.Where(b => b.Value.FullName == type).Select(b => b.Key).FirstOrDefault();
      return key;
    }
    public ModelPostModel GetModelPostModelByType(Type type)
    {
      return type.GetModelPostModelByType();
    }

    public IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where, out ISave repo)
    {
      var type = GetModelType(typeString);
      return Read<T>(type, where, out repo);
    }
    public IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where)
    {
      var result = Read<T>(typeString, where, out ISave repo);
      return result;
    }
    public IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where, ISave repo)
    {
      var type = GetModelType(typeString);
      return Read<T>(type, where, repo);
    }

    public object Find(string typeString, long id, out ISave repo)
    {
      var type = GetModelType(typeString);
      return Find(type, id, out repo);
    }
    public object Find(string typeString, long id)
    {
      var result = Find(typeString, id, out ISave repo);
      return result;
    }

    public void Delete(string key, long id)
    {
      try
      {
        var type = GetModelType(key);
        if (type == null)
        {
          return;
        }
        var model = Find(type, id, out var repo);
        if (model == null)
        {
          return;
        }
        Delete(model, repo);
      }
      catch { }
    }

    public ContentTableHtmlView GetContentTableHtmlView(Type type)
    {
      var children = Read<IInt64Key>(type, b => true, out ISave repo).ToList();
      var allowChild = type.GetObjectCustomAttribute<AllowChildrenAttribute>();

      IEnumerable<string> additionalList = allowChild != null && allowChild.TableList != null ? allowChild.TableList : new string[] { };
      var rowItems = children.Select(b =>
      {
        var values = additionalList.Select(a => b.GetPropertyByKey(a)).ToList();
        return new ContentTableRowItem(b.Id, values, b.GetType().GetRealType());
      }).ToList();
      var result = new ContentTableHtmlView();
      if (allowChild != null && allowChild.DisableDelete)
      {
        result.DisableDelete = true;
      }
      result.TableHeaders = additionalList.Select(b => type.GetPropertyLabelByKey(b)).ToList();
      result.Rows = rowItems;
      result.ThisTypeFrom = type;
      return result;
    }
    public ContentTableHtmlView GetContentTableHtmlView<T>(Type type, Expression<Func<T, bool>> where) where T : IInt64Key
    {
      var children = Read<T>(type, where, out ISave repo).ToList();
      var allowChild = type.GetObjectCustomAttribute<AllowChildrenAttribute>();

      IEnumerable<string> additionalList = allowChild != null && allowChild.TableList != null ? allowChild.TableList : new string[] { };
      var rowItems = children.Select(b =>
      {
        var values = additionalList.Select(a => b.GetPropertyByKey(a)).ToList();
        return new ContentTableRowItem(b.Id, values, b.GetType().GetRealType());
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

  }
}
