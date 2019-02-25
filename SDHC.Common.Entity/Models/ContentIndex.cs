using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDHC.Common.Entity.Models
{
  public class ContentIndex
  {
    public ContentIndex()
    {
      this.thisRepo = repo();
    }
    public static Func<IContentIndex> repo { get; set; }
    [Key]
    public Guid Id { get; set; }
    public Int64 ModelId { get; set; }

    public Guid? ParentId { get; set; }
    public string PageURL { get; set; }
    public string FullType { get; set; }

    public IContentIndex thisRepo { get; set; }

    public IQueryable<ContentIndex> Children()
    {
      return repo().contentIndexs.Where(b => b.ParentId == this.Id);
    }
    public static void Create(IBasicContent currentContent, Guid? parentId, out ContentIndex contentIndex)
    {
      var db = repo();
      var currentIndex = new ContentIndex()
      {
        Id = Guid.NewGuid(),
        ModelId = currentContent.Id,
        ParentId = parentId,
        PageURL = "",
        FullType = currentContent.GetType().FullName,
      };
      db.contentIndexs.Add(currentIndex);
      db.SaveChanges();
      contentIndex = currentIndex;
    }
    public static ContentIndex ReadByintId(long parengId, string fullType)
    {
      return repo().contentIndexs.Where(b => b.ModelId == parengId && b.FullType == fullType).FirstOrDefault();
    }
    public static void Move(Guid id, Guid? parentId)
    {
      var thisRepo = repo();
      var thisIndex = thisRepo.contentIndexs.Where(b => b.Id == id).FirstOrDefault();
      thisIndex.ParentId = parentId;
      thisRepo.SaveChanges();
    }
    public static void Move(long id, string type, Guid? parentId)
    {
      var thisRepo = repo();
      var thisIndex = thisRepo.contentIndexs.Where(b => b.ModelId == id && b.FullType == type).FirstOrDefault();
      thisIndex.ParentId = parentId;
      thisRepo.SaveChanges();
    }
  }

  public interface IContentIndex : ISave
  {
    DbSet<ContentIndex> contentIndexs { get; set; }
  }
  public interface ISave
  {
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync();
  }
}
