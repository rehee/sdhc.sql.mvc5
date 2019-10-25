using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class ModelManager
  {
    public static Dictionary<string, Type> ModelMapper { get; set; } = new Dictionary<string, Type>();
    public static IEnumerable<string> ModelManagerMapper { get; set; } = Enumerable.Empty<string>();
    public static Type GetModelType(string typeKey)
    {
      if (string.IsNullOrEmpty(typeKey))
      {
        return null;
      }
      if (ModelMapper == null || !ModelMapper.ContainsKey(typeKey))
      {
        return null;
      }
      return ModelManager.ModelMapper[typeKey];
    }
    public static string GetMapperKey(string type)
    {
      if (ModelMapper == null)
      {
        return null;
      }
      var key = ModelMapper.Where(b => b.Value.FullName == type).Select(b => b.Key).FirstOrDefault();
      return key;
    }

    public static ModelPostModel GetModelPostModelByType(Type type)
    {
      return type.GetModelPostModelByType();
    }

    public static void Create<T>(T input) where T : class
    {
      BaseModelCRUD.Create<T>(input);
    }
    public static void Create(object input)
    {
      BaseModelCRUD.Create(input);
    }
    public static void Create(ModelPostModel model)
    {
      BaseModelCRUD.Create(model);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave repo) where T : class
    {
      return BaseModelCRUD.Read<T>(where, out repo);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : class
    {
      return BaseModelCRUD.Read<T>(where, out var repo);
    }
    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, ISave repo) where T : class
    {
      return BaseModelCRUD.Read<T>(where, repo);
    }

    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, out ISave repo)
    {
      return BaseModelCRUD.Read<T>(type, where, out repo);
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where)
    {
      return Read<T>(type, where, out ISave repo);

    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, ISave repo)
    {
      return BaseModelCRUD.Read<T>(type, where, repo);
    }

    public static IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where, out ISave repo)
    {
      var type = GetModelType(typeString);
      return Read<T>(type, where, out repo);
    }
    public static IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where)
    {
      var result = Read<T>(typeString, where, out ISave repo);
      return result;
    }
    public static IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where, ISave repo)
    {
      var type = GetModelType(typeString);
      return Read<T>(type, where, repo);
    }
    //todo找时间需要把find也放入cuid
    public static T Find<T>(long id, out ISave repo) where T : class, IInt64Key
    {
      var result = Read<T>(b => b.Id == id, out repo).FirstOrDefault();
      return result;
    }
    public static T Find<T>(long id) where T : class, IInt64Key
    {
      var result = Read<T>(b => b.Id == id, out ISave repo).FirstOrDefault();
      return result;
    }
    
    public static object Find(Type type, long id, out ISave repo)
    {
      var result = Read<IInt64Key>(type, b => b.Id == id, out repo).FirstOrDefault();
      return result;
    }
    public static object Find(Type type, long id)
    {
      var result = Find(type, id, out ISave repo);
      return result;
    }

    public static object Find(string typeString, long id, out ISave repo)
    {
      var type = GetModelType(typeString);
      var set = BaseCruds.GetDbSet(type, out repo);
      var q = Queryable.Where<IInt64Key>((IQueryable<IInt64Key>)set, b => b.Id == id);
      return q.FirstOrDefault();
    }
    public static object Find(string typeString, long id)
    {
      var result = Find(typeString, id, out ISave repo);
      return result;
    }

    public static void Update(ModelPostModel model)
    {
      BaseModelCRUD.Update(model);
    }
    public static void Update(BaseViewModel model)
    {
      BaseModelCRUD.Update(model);
    }


    public static void Delete<T>(ISave repo, T model) where T : class, IInt64Key
    {
      var tType = typeof(T);
      var modelType = model.GetType().GetRealType();
      if (tType != modelType)
      {
        Delete(model as object, repo);
        return;
      }
      if (repo == null)
      {
        var id = model.Id;
        model = Find<T>(id, out repo);
      }
      var set = BaseCruds.GetDbSet<T>(repo) as DbSet<T>;
      set.Remove(model);
      repo.SaveChanges();
    }
    public static void Delete<T>(T model, ISave repo = null) where T : class, IInt64Key
    {
      Delete<T>(repo, model);
    }
    public static void Delete(object model, ISave repo = null)
    {
      var type = model.GetType().GetRealType();
      if (repo == null)
      {
        var id = ((IInt64Key)model).Id;
        model = Find(type, id, out repo);
      }
      var method = repo.GetMethod(type, "Remove", out object p);
      if (method != null)
      {
        try
        {
          method.Invoke(p, new object[1] { model });
          repo.SaveChanges();
        }
        catch
        {

        }
      }
    }
    public static void Delete(string key, long id)
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

    public static ContentTableHtmlView GetContentTableHtmlView(Type type)
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
    public static ContentTableHtmlView GetContentTableHtmlView<T>(Type type, Expression<Func<T, bool>> where) where T : IInt64Key
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
