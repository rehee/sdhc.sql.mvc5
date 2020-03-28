using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using System.Linq;
using System.Linq.Expressions;

namespace System
{
  public class CrudContent : CrudModel, ICrudContent
  {
    public CrudContent(ICrudInit container) : base(container)
    {
      this.BaseIContentModelType = container.BaseIContentModelType;
    }
    public Type BaseIContentModelType { get; private set; }
    public void Create(IContentModel input)
    {
      var db = GetRepo();
      var IQueable = db.GetDbSet(BaseIContentModelType);
      var method = db.GetMethod(BaseIContentModelType, "Add", out var IContent);
      method.Invoke(IContent, new object[1] { input });
      db.SaveChanges();
    }

    public T Read<T>(long id) where T : class, IContentModel
    {
      return GetByPK<T>(id, out ISave db);
    }

    public IQueryable<T> ReadContent<T>(Expression<Func<T, bool>> where) where T : IContentModel
    {

      var dbset = GetRepo().GetDbSet(typeof(T));
      if (dbset == null)
      {
        return null;
      }
      var a = dbset as IQueryable<T>;
      return Queryable.Where<T>(a, where);
    }
    public IQueryable<IInt64Key> Read(Type type, Expression<Func<IInt64Key, bool>> where)
    {
      var dbset = GetRepo().GetDbSet(type);
      if (dbset == null)
      {
        return null;
      }
      var a = dbset as IQueryable<IInt64Key>;
      return Queryable.Where<IInt64Key>(a, where);
    }
    
    public void Delete(long id, ISave db = null)
    {
      if (db == null)
      {
        db = GetRepo();
      }
      var model = Read<IContentModel>(BaseIContentModelType, b => b.Id == id, db).FirstOrDefault();
      if (model == null)
      {
        return;
      }
      var childrens = Read<IContentModel>(BaseIContentModelType, b => b.ParentId == id, db).ToList();
      if (childrens.Count > 0)
      {
        childrens.ForEach(b => Delete(b.Id, db));
      }
      Delete(model, db);
    }
    public T GetByPK<T>(long id, out ISave db) where T : class, IContentModel
    {
      db = GetRepo();
      return Read<T>(BaseIContentModelType, b => b.Id == id, db).FirstOrDefault();
    }
  }
}


