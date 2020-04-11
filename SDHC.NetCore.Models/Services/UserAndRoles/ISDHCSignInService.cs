using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SDHC.NetCore.Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.NetCore.Models.Services.UserAndRoles
{
  public interface ISDHCSignInService
  {
    Task<bool> CanSignInAsync(dynamic user);
    Task<SignInResult> CheckPasswordSignInAsync(dynamic user, string password, bool lockoutOnFailure);
    Task<dynamic> GetTwoFactorAuthenticationUserAsync();
    Task<bool> IsTwoFactorClientRememberedAsync(dynamic user);
    Task<ClaimsPrincipal> CreateUserPrincipalAsync(dynamic user);


    AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null);
    Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
    Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);
    Task ForgetTwoFactorClientAsync();
    Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
    Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);
    bool IsSignedIn(ClaimsPrincipal principal);

    Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
    Task<SignInResult> PasswordSignInAsync(dynamic user, string password, bool isPersistent, bool lockoutOnFailure);
    Task RefreshSignInAsync(dynamic user);
    Task RememberTwoFactorClientAsync(dynamic user);
    Task SignInAsync(dynamic user, bool isPersistent, string authenticationMethod = null);

    Task SignInAsync(dynamic user, AuthenticationProperties authenticationProperties, string authenticationMethod = null);
    Task SignInWithClaimsAsync(dynamic user, AuthenticationProperties authenticationProperties, IEnumerable<Claim> additionalClaims);
    Task SignInWithClaimsAsync(dynamic user, bool isPersistent, IEnumerable<Claim> additionalClaims);
    Task SignOutAsync();
    Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);
    Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);
    Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient);
    Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin);
    Task<dynamic> ValidateSecurityStampAsync(ClaimsPrincipal principal);
    Task<bool> ValidateSecurityStampAsync(dynamic user, string securityStamp);
    Task<dynamic> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal);
  }


  public class SDHCSignInService<T> : ISDHCSignInService where T : SDHCUser, new()
  {
    private SignInManager<T> signInManager { get; }
    public SDHCSignInService(SignInManager<T> signInManager)
    {
      this.signInManager = signInManager;
    }

    public Task<bool> CanSignInAsync(dynamic user)
    {
      return signInManager.CanSignInAsync(user);
    }

    public Task<SignInResult> CheckPasswordSignInAsync(dynamic user, string password, bool lockoutOnFailure)
    {
      return signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    public async Task<dynamic> GetTwoFactorAuthenticationUserAsync()
    {
      return await signInManager.GetTwoFactorAuthenticationUserAsync();
    }

    public Task<bool> IsTwoFactorClientRememberedAsync(dynamic user)
    {
      return signInManager.IsTwoFactorClientRememberedAsync(user);
    }

    public Task<ClaimsPrincipal> CreateUserPrincipalAsync(dynamic user)
    {
      return signInManager.CreateUserPrincipalAsync(user);
    }

    public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null)
    {
      return signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
    }

    public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
    {
      return signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);
    }

    public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
    {
      return signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
    }

    public Task ForgetTwoFactorClientAsync()
    {
      return signInManager.ForgetTwoFactorClientAsync();
    }

    public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
    {
      return signInManager.GetExternalAuthenticationSchemesAsync();
    }

    public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null)
    {
      return signInManager.GetExternalLoginInfoAsync(expectedXsrf);
    }

    public bool IsSignedIn(ClaimsPrincipal principal)
    {
      return signInManager.IsSignedIn(principal);
    }

    public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
      return signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
    }

    public Task<SignInResult> PasswordSignInAsync(dynamic user, string password, bool isPersistent, bool lockoutOnFailure)
    {
      return signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }

    public Task RefreshSignInAsync(dynamic user)
    {
      return signInManager.RefreshSignInAsync(user);
    }

    public Task RememberTwoFactorClientAsync(dynamic user)
    {
      return signInManager.RememberTwoFactorClientAsync(user);
    }

    public Task SignInAsync(dynamic user, bool isPersistent, string authenticationMethod = null)
    {
      return signInManager.SignInAsync(user, isPersistent, authenticationMethod);
    }

    public Task SignInAsync(dynamic user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
    {
      return signInManager.SignInAsync(user, authenticationProperties, authenticationMethod);
    }

    public Task SignInWithClaimsAsync(dynamic user, AuthenticationProperties authenticationProperties, IEnumerable<Claim> additionalClaims)
    {
      return signInManager.SignInWithClaimsAsync(user, authenticationProperties, additionalClaims);
    }

    public Task SignInWithClaimsAsync(dynamic user, bool isPersistent, IEnumerable<Claim> additionalClaims)
    {
      return signInManager.SignInWithClaimsAsync(user, isPersistent, additionalClaims);
    }

    public Task SignOutAsync()
    {
      return signInManager.SignOutAsync();
    }

    public Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient)
    {
      return signInManager.TwoFactorAuthenticatorSignInAsync(code, isPersistent, rememberClient);
    }

    public Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
    {
      return signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
    }

    public Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient)
    {
      return signInManager.TwoFactorSignInAsync(provider, code, isPersistent, rememberClient);
    }

    public Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin)
    {
      return signInManager.UpdateExternalAuthenticationTokensAsync(externalLogin);
    }

    public async Task<dynamic> ValidateSecurityStampAsync(ClaimsPrincipal principal)
    {
      return await signInManager.ValidateSecurityStampAsync(principal);
    }

    public Task<bool> ValidateSecurityStampAsync(dynamic user, string securityStamp)
    {
      return signInManager.ValidateSecurityStampAsync(user, securityStamp);
    }

    public async Task<dynamic> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal)
    {
      return await signInManager.ValidateTwoFactorSecurityStampAsync(principal);
    }
  }

}
