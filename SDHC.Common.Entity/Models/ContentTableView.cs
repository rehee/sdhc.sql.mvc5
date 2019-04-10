using SDHC.Common.Entity.Attributes;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public class ContentTableView
  {
    public IEnumerable<BaseContent> Contents { get; set; } = Enumerable.Empty<BaseContent>();
    public string RowClickFunction { get; set; } = "";
    public IEnumerable<ItemKeyAndName> Keys { get; set; } = Enumerable.Empty<ItemKeyAndName>();
    public ContentTableView(IEnumerable<BaseContent> contents, string rowClickFunction = "", Type contentType = null)
    {
      if (contents != null)
        Contents = contents;
      if (!string.IsNullOrEmpty(rowClickFunction))
        RowClickFunction = rowClickFunction;
      if (contentType != null)
      {
        Keys = new List<ItemKeyAndName>();
        var listItem = contentType.GetObjectCustomAttribute<ListItemAttribute>();
        if (listItem != null && listItem.KeyAndDisplayNames != null)
        {
          foreach (var k in listItem.KeyAndDisplayNames)
          {
            var kAndNames = k.Split(',');
            ((IList<ItemKeyAndName>)Keys).Add(new ItemKeyAndName(
              kAndNames[0], kAndNames.Count() > 1 ? kAndNames[1] : kAndNames[0]));
          }
        }
      }
    }
  }
  public class ContentTableHtmlView
  {
    public string FirstRowAction { get; set; }
    public string FirstRowController { get; set; }
    public string FirstRowArea { get; set; }
    public Func<object, object> FirstRowObject { get; set; }
    public string TableClass { get; set; }

    public IEnumerable<string> TableHeaders { get; set; }
    public IEnumerable<ContentTableRowItem> Rows { get; set; }


    public string FirstRowTitle { get; set; }
    public ContentTableOption FirstRow { get; set; }
    public List<ContentTableOption> Options { get; set; }

    public bool FirstRowIsNotAction { get; set; }
    public bool DisableDelete { get; set; }
    public string DeleteFunctionName { get; set; }
  }

  public class ContentTableOption
  {
    public Func<object, object> RowTitle { get; set; }
    public string ButtonClass { get; set; } = "";
    public string RowAction { get; set; }
    public string RowController { get; set; }
    public string RowArea { get; set; } = "";
    public Func<object, object> RowObject { get; set; }
    public Func<object, string> UrlAttribute { get; set; } = (o) => "";

  }

  public class ContentTableRowItem
  {
    public ContentTableRowItem(long id, IEnumerable<string> values, Type type, long displayOrder = 0, string stringId = "")
    {
      this.Id = id;
      this.Values = values;
      this.ThisType = type.GetRealType();
      this.DisplayOrder = displayOrder;
      this.StringId = stringId;
    }
    public long Id { get; set; }
    public long DisplayOrder { get; set; }
    public IEnumerable<string> Values { get; set; } = Enumerable.Empty<string>();
    public Type ThisType { get; set; }
    public string StringId { get; set; }
  }

  public class ItemKeyAndName
  {
    public string Key { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public ItemKeyAndName(string key, string displayName = null)
    {
      Key = key;
      if (string.IsNullOrEmpty(displayName))
      {
        DisplayName = key;
      }
      else
      {
        DisplayName = displayName;
      }
    }
  }
}
