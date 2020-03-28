using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SDHC.Common.Cruds
{
  public interface ICrud
  {
    Func<ISave> GetRepo { get; }
    void Create(IInt64Key model, ISave repo);
    void Create<T>(T input) where T : class;
    void Create(object input, out ISave repo);
    void Create(object input);
    void Create(object input, ISave repo);
    IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave db) where T : class;
    IQueryable<T> Read<T>(Expression<Func<T, bool>> where) where T : class;
    IQueryable<T> Read<T>(Expression<Func<T, bool>> where, ISave db) where T : class;
    IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, out ISave db);
    IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where);
    IQueryable<T> Read<T>(Type type, Expression<Func<T, bool>> where, ISave db);

    void Update<T>(T input) where T : class, IInt64Key;
    void Update(Type type, IInt64Key input);

    T Find<T>(long id, out ISave repo) where T : class, IInt64Key;
    T Find<T>(long id) where T : class, IInt64Key;
    T Find<T>(Type type, long id, out ISave repo) where T : class, IInt64Key;
    T Find<T>(Type type, long id) where T : class, IInt64Key;
    object Find(Type type, long id, out ISave repo);
    object Find(Type type, long id);
    void Delete<T>(ISave repo, T model) where T : class, IInt64Key;
    void Delete<T>(T model, ISave repo = null) where T : class, IInt64Key;
    void Delete(object model, ISave repo = null);
  }
}

