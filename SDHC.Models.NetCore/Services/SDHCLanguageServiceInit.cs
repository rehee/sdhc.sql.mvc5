﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SDHC.Common.Configs;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDHC.Common.EntityCore.Services
{
  public class SDHCLanguageServiceInit : ISDHCLanguageServiceInit
  {
    private IHttpContextAccessor acce { get; }
    public LanguageConfig config { get; }
    private ISession session { get; }
    public SDHCLanguageServiceInit(IHttpContextAccessor acce, IOptions<LanguageConfig> config)
    {
      this.acce = acce;
      this.session = acce != null && acce.HttpContext != null && acce.HttpContext.Session != null ? acce.HttpContext.Session : null;
      this.config = config.Value;
    }
    public string LanguageKey => config.LanguageKey;
    public Func<string, object> getSession => (key) =>
    {
      if (session == null)
      {
        return config.DefaultLanguage.Key;
      }
      return session.GetInt32(key);
    };
    public Action<string, object> setSession => (key, value) =>
    {
      if (this.session == null)
        return;
      session.SetInt32(key, (int)value);
    };
  }
}
