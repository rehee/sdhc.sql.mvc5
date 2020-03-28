using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SDHC.Crud.Services
{
  public interface ICrud
  {
    void Create(object input);
    void Create<T>(T input);
    void Create(ModelPostModel model);

    IQueryable<T> Read<T>(Expression<Func<T, bool>> where);
    IQueryable<T> Read<T>(Expression<Func<T, bool>> where, out ISave repo);
    IQueryable<T> Read<T>(Expression<Func<T, bool>> where, ISave repo);

  }
}
