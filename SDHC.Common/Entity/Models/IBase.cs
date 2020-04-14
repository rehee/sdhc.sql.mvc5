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
  public interface IBasicContent : IInt64Key, IDisplayName
  {

  }
  public interface ISharedContent : IBasicModel
  {
    int Lang { get; set; }
  }
  public interface ISharedList : ISharedContent
  {
    int DisplayOrder { get; set; }
    bool Displaied { get; set; }
  }
  public interface IBasicModel : IBasicContent
  {
    string Title { get; set; }
  }
  public interface IContentModel : IBasicModel
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
  public interface IBasicSelect : IBasicContent
  {

  }
}
