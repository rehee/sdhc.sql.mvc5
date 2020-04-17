using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Common.Cruds
{
  public interface ICrudModel : ICrud
  {
    void Create(ModelPostModel model);
    void Update(ModelPostModel model);
    void Update(BaseViewModel model);

    Task CreateOrUpdate(ModelPostModel model);
  }
}

