using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SDHC.Common.Cruds;
using SDHC.Common.Entity.Extends;
using SDHC.Common.Entity.Models;
using SDHC.Common.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Start
{
  public static class SDHCStartup
  {
    public static void Init<TRepo, TBaseContent, TBaseSelect, TBaseUser>(
      IAppBuilder app, Func<TRepo> repoCreate, string webBasePath)
      where TRepo : DbContext, IContent, new()
      where TBaseContent : BaseContent
      where TBaseSelect : BaseSelect
      where TBaseUser : SDHCUser
    {
      ConfigureAuth<TRepo, TBaseUser>(app, repoCreate);
      var crudInit = new CrudInit(
        () => new TRepo(), typeof(TBaseContent)
        );

      CrudContainer.Crud = new BaseCruds(crudInit);
      CrudContainer.CrudModel = new CrudModel(crudInit);
      CrudContainer.CrudContent = new CrudContent(crudInit);
      ServiceContainer.ModelService = new ModelService(crudInit);
      ServiceContainer.ContentService = new ContentService(crudInit);

      SelectManager.BasicSelectType = typeof(TBaseSelect);
      FileManager.BasePath = webBasePath;
      SDHCUserManager.BaseUser = typeof(TBaseUser);

      ContentPostViewModel.GetContentPageUrl = () => G.ContentPageUrl;
      ContentPostViewModel.GetContentViewPath = () => G.ContentViewPath;
      ContentPostViewModel.Convert = (input) => input.ConvertModelToPost();

      ServiceContainer.SDHCFileService = new SDHCFileService(new SDHCFileConfig(
        webBasePath, G.FileUploadPath, new Dictionary<Type, SDHCSaveAble>()
        {
          [typeof(HttpPostedFileBase)] = new SDHCSaveAble(
            (input) =>
            {
              if (input == null)
                return null;
              return (input as HttpPostedFileBase).FileName;
            }, (input, fileName) =>
             {
               if (input == null)
                 return;
               (input as HttpPostedFileBase).SaveAs(fileName);
             }),
          [typeof(HttpPostedFileBase[])] = new SDHCSaveAble(
            (input) =>
            {
              if (input == null)
                return null;
              return (input as HttpPostedFileBase[]).FirstOrDefault().FileName;
            }, (input, fileName) =>
            {
              if (input == null)
                return;
              (input as HttpPostedFileBase[]).FirstOrDefault().SaveAs(fileName);
            }),
        }));
    }
    // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
    public static void ConfigureAuth<T, TUser>(IAppBuilder app, Func<T> create) where T : DbContext where TUser : SDHCUser
    {
      // Configure the db context, user manager and signin manager to use a single instance per request
      app.CreatePerOwinContext(create);
      app.CreatePerOwinContext<ApplicationUserManagerG<TUser>>(ApplicationUserManagerG<TUser>.Create<T>);
      app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create<T>);
      app.CreatePerOwinContext<ApplicationSignInManagerG<TUser>>(ApplicationSignInManagerG<TUser>.Create);
      app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
      //app.CreatePerOwinContext<ApplicationSignInManager<S>>(ApplicationSignInManager<TUser>.Create);
      app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create<T>);

      // Enable the application to use a cookie to store information for the signed in user
      // and to use a cookie to temporarily store information about a user logging in with a third party login provider
      // Configure the sign in cookie
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login"),
        Provider = new CookieAuthenticationProvider
        {
          // Enables the application to validate the security stamp when the user logs in.
          // This is a security feature which is used when you change a password or add an external login to your account.  
          OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManagerG<TUser>, TUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
        }
      });
      app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

      // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
      app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

      // Enables the application to remember the second login verification factor such as phone or email.
      // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
      // This is similar to the RememberMe option when you log in.
      app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

      // Uncomment the following lines to enable logging in with third party login providers
      //app.UseMicrosoftAccountAuthentication(
      //    clientId: "",
      //    clientSecret: "");

      //app.UseTwitterAuthentication(
      //   consumerKey: "",
      //   consumerSecret: "");

      //app.UseFacebookAuthentication(
      //   appId: "",
      //   appSecret: "");

      //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
      //{
      //    ClientId = "",
      //    ClientSecret = ""
      //});
    }
  }
}
