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
      var baseModel = Activator.CreateInstance(type);
      return baseModel.ConvertModelToModelPostModel();
    }

    public static void Create<T>(T input) where T : class
    {
      var type = typeof(T);
      var set = BaseCruds.GetDbSet<T>(out ISave repo) as DbSet<T>;
      set.Add(input);
      repo.SaveChanges();
    }
    public static void Create(object input)
    {
      var type = input.GetType();
      var repo = BaseCruds.GetRepo();
      var addMethod = repo.GetMethod(type, "Add", out object p);
      if (addMethod != null)
      {
        try
        {
          addMethod.Invoke(p, new object[1] { input });
          repo.SaveChanges();
        }
        catch
        {

        }
      }
    }
    public static void Create(ModelPostModel model)
    {
      var obj = model.ConvertToBaseModel();
      Create(obj);
    }

    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave repo) where T : class
    {
      var set = BaseCruds.GetDbSet<T>(out var raepo);
      return BaseCruds.GetDbSet<T>(out repo).Where(where);
    }

    public static IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : class
    {
      var result = Read<T>(where, out ISave repo);
      return result;
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, out ISave repo)
    {
      var tType = typeof(T);
      
      var set = BaseCruds.GetDbSet(type, out repo);
      var q = Queryable.Where<T>((IQueryable<T>)set, where);
      return q;
    }
    public static IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where)
    {
      var result = Read<T>(type, where, out ISave repo);
      return result;
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
      var type = Type.GetType($"{model.FullType},{model.ThisAssembly}");
      var target = Read<IInt64Key>(type, b => b.Id == model.Id, out ISave repo).FirstOrDefault();
      model.ConvertToBaseModel(target);
      repo.SaveChanges();
    }

    public static void Delete<T>(ISave repo, T model) where T : class, IInt64Key
    {
      var tType = typeof(T);
      var modelType = model.GetType();
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
      var type = model.GetType();
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

  }
}
