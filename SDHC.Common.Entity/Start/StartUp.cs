using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Start
{
  public static class SDHCStartup
  {
    // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
    public static void ConfigureAuth<T>(IAppBuilder app,Func<T> create) where T:DbContext
    {
      // Configure the db context, user manager and signin manager to use a single instance per request
      app.CreatePerOwinContext(create);
      app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create<T>);
      app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
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
          OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, SDHCUser>(
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
