using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Services
{
  public interface IViewNameService
  {
    string ViewName(ILanguage model);
    string ViewName(ContentPostModel model);
    string ViewName(ContentViewModal model);
    string ViewName(ContentPropertyIndex model);
  }
}
