using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace System
{
  public class BaseCruds : ICrud
  {
    public BaseCruds(ICrudInit container)
    {
      this.GetRepo = container.GetRepo;
    }
    public virtual Func<ISave> GetRepo { get; }

    public virtual void Create(IInt64Key model, ISave repo)
    {
      if (model == null || repo == null)
        return;
      var type = model.GetType().GetRealType();
      var repoType = repo.GetType();
      var addMethod = repo.GetMethod(type, "Add", out object p);
      if (addMethod != null)
      {
        try
        {
          addMethod.Invoke(p, new object[1] { model });
          repo.SaveChanges();
        }
        catch
        {

        }
      }
    }
    public void Create<T>(T input) where T : class
    {
      if (input == null)
        return;
      var type = typeof(T);
      var repo = this.GetRepo();
      var set = repo.GetDbSet<T>();
      var addFunc = set.GetMethod("Add");
      addFunc.Invoke(set, new object[1] { input });
      repo.SaveChanges();
    }
    public virtual IQueryable<object> GetDbSet(Type type, out ISave repo)
    {
      repo = GetRepo();
      var set = repo.GetDbSet(type);
      return (IQueryable<object>)set;
    }
    public virtual void Create(object input, out ISave repo)
    {
      repo = GetRepo();
      Create(input, repo);
    }
    public virtual void Create(object input)
    {
      Create(input, out var repo);
    }
    public virtual void Create(object input, ISave repo)
    {

      var type = input.GetType().GetRealType();
      var repoType = repo.GetType().GetRealType();
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
    public virtual IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave db) where T : class
    {
      db = GetRepo();
      return Read<T>(where, db);
    }
    public virtual IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : class
    {
      return Read<T>(where, out var repo);
    }
    public virtual IQueryable<T> Read<T>(Expression<Func<T, bool>> where, ISave db) where T : class
    {
      var dbset = db.GetDbSet<T>();
      if (dbset == null)
        return null;
      return Queryable.Where<T>(dbset, where);
    }
    public virtual IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, out ISave db)
    {
      db = GetRepo();
      return Read<T>(type, where, db);
    }
    public virtual IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where)
    {
      return Read<T>(type, where, out var repo);
    }
    public virtual IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, ISave db)
    {
      var dbset = db.GetDbSet(type) as IQueryable<T>;
      if (dbset == null)
        return null;
      return Queryable.Where<T>(dbset, where);
    }
    public void Update<T>(T input) where T : class, IInt64Key
    {
      var model = Find<T>(input.Id, out ISave db);
      setUpdate(db, typeof(T), model, input);
    }
    public void Update(Type type, IInt64Key input)
    {
      var model = Find(type, input.Id, out ISave db);
      setUpdate(db, type, model, input);

    }
    protected void setUpdate(ISave db, Type type, object model, object input)
    {
      if (model == null)
      {
        return;
      }
      foreach (var p in type.GetProperties())
      {
        try
        {
          p.SetValue(model, p.GetValue(input));
        }
        catch { }
      }
      db.SaveChanges();
    }
    public virtual T Find<T>(long id, out ISave repo) where T : class, IInt64Key
    {
      var result = Read<T>(b => b.Id == id, out repo).FirstOrDefault();
      return result;
    }
    public virtual T Find<T>(long id) where T : class, IInt64Key
    {
      var result = Read<T>(b => b.Id == id, out ISave repo).FirstOrDefault();
      return result;
    }
    public virtual T Find<T>(Type type, long id, out ISave repo) where T : class, IInt64Key
    {
      var result = Read<T>(type, b => b.Id == id, out repo).FirstOrDefault();
      return result;
    }
    public virtual T Find<T>(Type type, long id) where T : class, IInt64Key
    {
      var result = Read<T>(type, b => b.Id == id).FirstOrDefault();
      return result;
    }
    public virtual object Find(Type type, long id, out ISave repo)
    {
      var result = Read<IInt64Key>(type, b => b.Id == id, out repo).FirstOrDefault();
      return result;
    }
    public virtual object Find(Type type, long id)
    {
      var result = Find(type, id, out ISave repo);
      return result;
    }
    public virtual void Delete<T>(ISave repo, T model) where T : class, IInt64Key
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
        var deleteModel = Find<T>(id, out repo);
        if (deleteModel == null)
          return;
        Delete(deleteModel as object, repo);
      }
    }
    public virtual void Delete<T>(T model, ISave repo = null) where T : class, IInt64Key
    {
      Delete<T>(repo, model);
    }
    public virtual void Delete(object model, ISave repo = null)
    {
      var type = model.GetType().GetRealType();
      if (!type.GetInterfaces().Any(b => b == typeof(IInt64Key)))
        return;
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

    public Task CreateAsync(IInt64Key model, ISave repo)
    {
      return Task.Run(() =>
      {
        Create(model, repo);
      });
    }

    public Task CreateAsync<T>(T input) where T : class
    {
      return Task.Run(() =>
      {
        Create(input);
      });
    }

    public Task CreateAsync(object input, out ISave repo)
    {
      var thisRepo = GetRepo();
      repo = thisRepo;
      return CreateAsync(input, thisRepo);
    }

    public Task CreateAsync(object input)
    {
      return Task.Run(() =>
      {
        Create(input);
      });
    }

    public Task CreateAsync(object input, ISave repo)
    {
      return Task.Run(() =>
      {
        Create(input, repo);
      });
    }

    public Task<IQueryable<T>> ReadAsync<T>(Expression<Func<T, bool>> where, out ISave db) where T : class
    {
      var thisDb = GetRepo();
      db = GetRepo();
      var task = new Task<IQueryable<T>>(() =>
      {
        return Read<T>(where, thisDb);
      });
      task.Start();
      return task;
    }

    public Task<IQueryable<T>> ReadAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
      var task = new Task<IQueryable<T>>(() =>
      {
        return Read<T>(where);
      });
      task.Start();
      return task;
    }

    public Task<IQueryable<T>> ReadAsync<T>(Expression<Func<T, bool>> where, ISave db) where T : class
    {
      var task = new Task<IQueryable<T>>(() =>
      {
        return Read<T>(where, db);
      });
      task.Start();
      return task;
    }

    public Task<IQueryable<T>> ReadAsync<T>(Type type, Expression<Func<T, bool>> where, out ISave db)
    {
      var thisDb = GetRepo();
      db = thisDb;
      var task = new Task<IQueryable<T>>(() =>
      {
        return Read<T>(type, where, thisDb);
      });
      task.Start();
      return task;
    }

    public Task<IQueryable<T>> ReadAsync<T>(Type type, Expression<Func<T, bool>> where)
    {
      var task = new Task<IQueryable<T>>(() =>
      {
        return Read<T>(type, where);
      });
      task.Start();
      return task;
    }

    public Task<IQueryable<T>> ReadAsync<T>(Type type, Expression<Func<T, bool>> where, ISave db)
    {
      var task = new Task<IQueryable<T>>(() =>
      {
        return Read<T>(type, where, db);
      });
      task.Start();
      return task;
    }

    public Task UpdateAsync<T>(T input) where T : class, IInt64Key
    {
      return Task.Run(() =>
      {
        Update<T>(input);
      });
    }

    public Task UpdateAsync(Type type, IInt64Key input)
    {
      return Task.Run(() =>
      {
        UpdateAsync(type, input);
      });
    }

    public Task<T> FindAsync<T>(long id, out ISave repo) where T : class, IInt64Key
    {
      var result = Find<T>(id, out repo);
      return Task.FromResult(result);
    }

    public Task<T> FindAsync<T>(long id) where T : class, IInt64Key
    {
      return Task<T>.Run(() =>
      {
        return Find<T>(id);
      });
    }

    public Task<T> FindAsync<T>(Type type, long id, out ISave repo) where T : class, IInt64Key
    {
      var result = Find<T>(type, id, out repo);
      return Task.FromResult(result);
    }

    public Task<T> FindAsync<T>(Type type, long id) where T : class, IInt64Key
    {
      return Task<T>.Run(() =>
      {
        return Find<T>(type, id);
      });
    }

    public Task<object> FindAsync(Type type, long id, out ISave repo)
    {
      var result = Find(type, id, out repo);
      return Task.FromResult(result);
    }

    public Task<object> FindAsync(Type type, long id)
    {
      return Task<object>.Run(() =>
      {
        return Find(type, id);
      });
    }

    Task ICrud.DeleteAsync<T>(ISave repo, T model)
    {
      return Task.Run(() =>
      {
        Delete<T>(repo, model);
      });
    }

    Task ICrud.DeleteAsync<T>(T model, ISave repo)
    {
      return Task.Run(() =>
      {
        Delete<T>(model, repo);
      });
    }

    public Task DeleteAsync(object model, ISave repo = null)
    {
      return Task.Run(() =>
      {
        Delete(model, repo);
      });
    }
  }
}
