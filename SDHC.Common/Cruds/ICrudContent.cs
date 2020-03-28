using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SDHC.Common.Cruds
{
  public interface ICrudContent : ICrudModel
  {
    void Create(IContentModel input);
    Type BaseIContentModelType { get; }
    T Read<T>(long id) where T : class, IContentModel;
    IQueryable<T> ReadContent<T>(Expression<Func<T, bool>> where) where T : IContentModel;

    IQueryable<IInt64Key> Read(Type type, Expression<Func<IInt64Key, bool>> where);
    void Delete(long id, ISave db = null);
    T GetByPK<T>(long id, out ISave db) where T : class, IContentModel;
  }
}

