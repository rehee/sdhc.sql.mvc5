using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
  public static class ServiceContainer
  {
    public static IModelService ModelService { get; set; }
    public static IContentService ContentService { get; set; }
    public static ISDHCFileService SDHCFileService { get; set; }
    public static ISelectService SelectService { get; set; }
    public static ISecretService SecretService { get; set; }
  }
}
