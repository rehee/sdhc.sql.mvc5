using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.Entity.Models
{
  public interface IInt64Key
  {
    Int64 Id { get; set; }
  }
  public interface IStringKey
  {
    String Id { get; set; }
  }
  public interface IDisplayName
  {
    string DisplayName();
  }
  public interface IBasicModel : IInt64Key, IDisplayName
  {
    string Title { get; set; }
  }
  public interface ISharedContent : IBasicModel
  {
    int Lang { get; set; }
  }
  public interface ISharedLink : ISharedContent
  {
    int DisplayOrder { get; set; }
    bool Displayed { get; set; }
  }
  public interface IBasicContent : IBasicModel
  {
    
  }
  public interface IContentModel : IBasicContent
  {
    int? Lang { get; set; }
    string Url { get; set; }
    long DisplayOrder { get; set; }
    DateTime? CreateTime { get; set; }
    long? ParentId { get; set; }
    IContentModel Parent { get; }
    IEnumerable<IContentModel> Parents { get; }
    IEnumerable<IContentModel> Children { get; }
  }
  public interface IBasicSelect : IBasicModel
  {

  }
}
