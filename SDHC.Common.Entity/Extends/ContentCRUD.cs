//using SDHC.Common.Entity.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SDHC.Common.Entity.Extends
//{
//  public static class ContentCRUD
//  {
//    //public static Func<IContent> repo { get; set; }
//    public static void CreateContent(this IBasicContent model, Guid? parentId = null)
//    {
//      //model.Create(repo());
//      //ContentIndex.Create(model, parentId, out ContentIndex index);
//    }
//    public static void CreateContentChildren(this IBasicContent parent, IBasicContent model)
//    {
//      CreateContent(model, parent.Id, parent.GetType().FullName);
//    }
//    public static void CreateContentParent(this IBasicContent model, IBasicContent parent)
//    {
//      CreateContent(model, parent.Id, parent.GetType().GetRealType().FullName);
//    }
//    public static void CreateContent(this IBasicContent model, Int64 parentId, string fullType)
//    {
//      Guid? parentGuid = null;
//      if (!String.IsNullOrEmpty(fullType))
//      {
//        //var parent = ContentIndex.ReadByintId(parentId, fullType);
//        //parentGuid = parent != null ? (Guid?)parent.Id : null;
//      }
//      CreateContent(model, parentGuid);
//    }
//    public static void Move(this IBasicContent model, Guid? parentId = null)
//    {
//      //ContentIndex.Move(model.Id, model.GetType().GetRealType().FullName, parentId);
//    }
//    public static void Move(this Guid id, Guid? parentId = null)
//    {

//    }
//    public static void Move(this IBasicContent model, IBasicContent parentId = null)
//    {

//    }
//    public static void Move(this IBasicContent model, Int64 parentId, string fullType)
//    {

//    }

//    public static void ReadId()
//    {

//    }
//    public static void Update()
//    {

//    }
//    public static void Delete()
//    {

//    }

//  }
//}
