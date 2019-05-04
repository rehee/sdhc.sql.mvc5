using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SDHC.Common.Entity.Models;

namespace System
{
  public class EmailService : IIdentityMessageService
  {
    public Task SendAsync(IdentityMessage message)
    {
      // Plug in your email service here to send an email.
      return Task.FromResult(0);
    }
  }

  public class SmsService : IIdentityMessageService
  {
    public Task SendAsync(IdentityMessage message)
    {
      // Plug in your SMS service here to send a text message.
      return Task.FromResult(0);
    }
  }

  public class ApplicationRoleManager : RoleManager<IdentityRole>
  {
    public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
    {

    }
    public static ApplicationRoleManager Create<T>(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context) where T : DbContext
    {
      return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<T>()));
    }
  }
  // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
  public class ApplicationUserManager : UserManager<SDHCUser>
  {

    public ApplicationUserManager(IUserStore<SDHCUser> store)
        : base(store)
    {
    }

    
    public static ApplicationUserManager Create<T>(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) where T : DbContext
    {
      ApplicationUserManager manager;
      //if (G.MongoDbIuserStore != null)
      //{
      //  manager = new ApplicationUserManager(G.MongoDbIuserStore());
      //}
      //else
      //{
      //  manager = new ApplicationUserManager(new UserStore<SDHCUser>(context.Get<T>()));
      //}
      
      manager = new ApplicationUserManager(new UserStore<SDHCUser>(context.Get<T>()));
      // Configure validation logic for usernames
      manager.UserValidator = new UserValidator<SDHCUser>(manager)
      {
        AllowOnlyAlphanumericUserNames = false,
        RequireUniqueEmail = true
      };

      // Configure validation logic for passwords
      manager.PasswordValidator = new PasswordValidator
      {
        RequiredLength = 1,
        RequireNonLetterOrDigit = false,
        RequireDigit = false,
        RequireLowercase = false,
        RequireUppercase = false,
      };

      // Configure user lockout defaults
      manager.UserLockoutEnabledByDefault = false;
      manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
      manager.MaxFailedAccessAttemptsBeforeLockout = 99999;

      // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
      // You can write your own provider and plug it in here.
      manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<SDHCUser>
      {
        MessageFormat = "Your security code is {0}"
      });
      manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<SDHCUser>
      {
        Subject = "Security Code",
        BodyFormat = "Your security code is {0}"
      });
      manager.EmailService = new EmailService();
      manager.SmsService = new SmsService();
      var dataProtectionProvider = options.DataProtectionProvider;
      if (dataProtectionProvider != null)
      {
        manager.UserTokenProvider =
            new DataProtectorTokenProvider<SDHCUser>(dataProtectionProvider.Create("ASP.NET Identity"));
      }
      return manager;
    }
  }

  // Configure the application sign-in manager which is used in this application.
  public class ApplicationSignInManager : SignInManager<SDHCUser, string>
  {
    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
    {
    }

    public override Task<ClaimsIdentity> CreateUserIdentityAsync(SDHCUser user)
    {
      return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    }

    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    {
      return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }
  }
}

namespace Start
{

}
