﻿using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SDHC.Common.Services
{
  public interface IModelService : ICrudModel
  {
    Dictionary<string, Type> ModelMapper { get; set; }
    IEnumerable<string> ModelManagerMapper { get; }
    Type GetModelType(string typeKey);
    string GetMapperKey(string type);

    ModelPostModel GetModelPostModelByType(Type type);
    IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where, out ISave repo);
    IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where);
    IQueryable<T> Read<T>(string typeString, Expression<Func<T, bool>> where, ISave repo);
    object Find(string typeString, long id, out ISave repo);
    object Find(string typeString, long id);
    void Delete(string key, long id);
    
    ContentTableHtmlView GetContentTableHtmlView(Type type);
    ContentTableHtmlView GetContentTableHtmlView<T>(Type type, Expression<Func<T, bool>> where) where T : IInt64Key;

  }
}