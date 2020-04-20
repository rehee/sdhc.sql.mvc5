using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDHC.NetCore.Models.Models.ViewModels
{
  public class SharedLinkView
  {
    public string Value { get; }
    public string ReferenceName { get; }
    public IEnumerable<ISharedLink> Models { get; set; }
    public SharedLinkView(string value, string listName)
    {
      Value = value;
      ReferenceName = listName;
    }
    public SharedLinkView(IEnumerable<ISharedLink> models, string listName)
    {
      Models = models;
      ReferenceName = listName;
    }
    public string ListName => $"List_{ReferenceName}";
    public string AppName => $"App_{ReferenceName}";

  }

  public class SharedLinkPost
  {
    public SharedLinkView View { get; set; }
    public IEnumerable<string> Headers { get; set; }
    public IEnumerable<string> Images { get; set; }
    public IEnumerable<ISharedLink> Models { get; set; }
    public String TypeName { get; set; }
    public String AssemblyName { get; set; }
    public bool IsRelated { get; set; }
    public long RelatedId { get; set; }
    public int? Lang { get; set; }
  }
}
