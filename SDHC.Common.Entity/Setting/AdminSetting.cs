﻿using Microsoft.AspNet.Identity;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static partial class G
  {
    [Config]
    public static bool UseContentRouter { get; set; } = false;
    [Config]
    public static bool AdminFree { get; set; } = false;
    [Config]
    public static int SortChildLevel { get; set; } = 4;
    [Config]
    public static string FileUploadPath { get; set; } = "files";

    [Config]
    public static string AdminTitle { get; set; } = "";
    [Config]
    public static string AdminCopyright { get; set; } = "";
    [Config]
    public static string AdminPath { get; set; } = "Admin";
    [Config]
    public static string SuperUserRole { get; set; } = "Admin";
    [Config]
    public static string AdminRole { get; set; } = "Admin";

    public static int DeleteMinTime { get; set; } = 1000;

    public static string GetModelTitle(this string key)
    {
      if (ModelManager.ModelMapper.ContainsKey(key))
      {
        var type = ModelManager.ModelMapper[key];

        var allow = type.GetAllowChildren();
        if (allow == null || String.IsNullOrEmpty(allow.Name))
        {
          return key.SpacesFromCamel();
        }
        return allow.Name.SpacesFromCamel();
      }

      return key;
    }
    public static string GetModelTitleFullType(string fullName, string assemName)
    {
      var type = Type.GetType($"{fullName},{assemName}");
      if (type != null)
      {
        var allow = type.GetAllowChildren();
        if (allow == null || String.IsNullOrEmpty(allow.Name))
        {
          return type.Name.SpacesFromCamel();
        }
        return allow.Name.SpacesFromCamel();
      }

      return "";
    }

    public static Func<IUserStore<SDHCUser>> MongoDbIuserStore { get; set; }
  }
}
