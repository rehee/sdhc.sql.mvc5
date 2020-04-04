﻿using SDHC.Common.Cruds;
using SDHC.Common.Entity.Models;
using SDHC.Common.Entity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SDHC.Common.Services
{
  public interface IContentService : ICrudContent
  {
    void CreateContent(IContentModel input, long? parentId = null);
    void UpdateContent(IContentModel input);
    void UpdateContent(ContentPostModel input);
    void MoveContent(long contentId, long? parentId);
    IEnumerable<IContentModel> GetAllChildContent(long? parentId, int? langKey = null);
    ContentTableHtmlView GetContentTableHtmlView(long? parentId, int? langKey = null);
    IContentModel GetContent(long? id);
    ContentPostModel GetPreCreate(long? id, string fullTypeAndAssembly, int? langKey);
    ContentPostViewModel GetContentPostViewModel(string url);
    string GetContentFullUrl(IContentModel model);
    ContentListView GetContentListView(long? id, int parentLevel = 0, int? langKey = null);
    void GetContentListView(IContentModel model, ContentListView parent, int parentLevel = 0, int sortChildLevel = 4);
    long? UpdateContentOrder(IEnumerable<ContentSortPostModel> inputs);

    ContentIndexViewModel<T> GetContentIndexViewModelByIdOrLang<T>(long? id, int? langKey, Func<string, bool> IsInRole) where T : IContentModel;
  }
}
