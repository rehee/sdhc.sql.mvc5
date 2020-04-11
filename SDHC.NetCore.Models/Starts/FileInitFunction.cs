using Microsoft.AspNetCore.Http;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class FileInitFunction
  {
    public static void FileServiceInit<TFileSngle>([NotNullAttribute] this IServiceCollection serviceCollection,
      string basicRoot
      ) where TFileSngle : IFormFile
    {
      ServiceContainer.SDHCFileService = new SDHCFileService(new SDHCFileConfig(
        basicRoot, ConfigContainer.Systems.FileUploadPath, new Dictionary<Type, SDHCSaveAble>()
        {
          [typeof(TFileSngle)] = new SDHCSaveAble(
            (input) =>
            {
              if (input == null)
                return null;
              return (input as IFormFile).FileName;
            }, (input, fileName) =>
            {
              if (input == null)
                return;
              using (var stream = System.IO.File.Create(fileName))
              {
                (input as IFormFile).CopyTo(stream);
              }
            }),
          [typeof(List<IFormFile>)] = new SDHCSaveAble(
            (input) =>
            {
              if (input == null)
                return null;
              return (input as IEnumerable<IFormFile>).FirstOrDefault().FileName;
            }, (input, fileName) =>
            {
              if (input == null)
                return;
              using (var stream = System.IO.File.Create(fileName))
              {
                (input as IEnumerable<IFormFile>).FirstOrDefault().CopyTo(stream);
              }
            }),
        }));
    }
  }
}
