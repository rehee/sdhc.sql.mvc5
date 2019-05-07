using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
  public interface IContent : ISave
  {
    IDbSet<BaseContent> Contents { get; set; }
    IDbSet<BaseSelect> Selects { get; set; }
  }
  
}
