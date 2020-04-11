using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SDHC.Models.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SDHC.Models.NetCore.Services.UserAndRoles
{
  public interface ISDHCMemberService
  {
    IQueryable<dynamic> Users { get; }
    IEnumerable<dynamic> UserValidators { get; }
    Task<IdentityResult> AccessFailedAsync(dynamic user);
    Task<IdentityResult> AddClaimAsync(dynamic user, Claim claim);
    Task<IdentityResult> AddClaimsAsync(dynamic user, IEnumerable<Claim> claims);
    Task<IdentityResult> AddLoginAsync(dynamic user, UserLoginInfo login);
    Task<IdentityResult> AddPasswordAsync(dynamic user, string password);
    Task<IdentityResult> AddToRoleAsync(dynamic user, string role);
    Task<IdentityResult> AddToRolesAsync(dynamic user, IEnumerable<string> roles);
    Task<IdentityResult> ChangeEmailAsync(dynamic user, string newEmail, string token);
    Task<IdentityResult> ChangePasswordAsync(dynamic user, string currentPassword, string newPassword);
    Task<IdentityResult> ChangePhoneNumberAsync(dynamic user, string phoneNumber, string token);
    Task<bool> CheckPasswordAsync(dynamic user, string password);
    Task<IdentityResult> ConfirmEmailAsync(dynamic user, string token);
    Task<int> CountRecoveryCodesAsync(dynamic user);
    Task<IdentityResult> CreateAsync(dynamic user);
    Task<IdentityResult> CreateAsync(dynamic user, string password);
    Task<byte[]> CreateSecurityTokenAsync(dynamic user);
    Task<IdentityResult> DeleteAsync(dynamic user);
    public void Dispose();
    Task<dynamic> FindByEmailAsync(string email);
    Task<dynamic> FindByIdAsync(string userId);
    Task<dynamic> FindByLoginAsync(string loginProvider, string providerKey);
    Task<dynamic> FindByNameAsync(string userName);
    Task<string> GenerateChangeEmailTokenAsync(dynamic user, string newEmail);
    Task<string> GenerateChangePhoneNumberTokenAsync(dynamic user, string phoneNumber);
    Task<string> GenerateConcurrencyStampAsync(dynamic user);
    Task<string> GenerateEmailConfirmationTokenAsync(dynamic user);
    string GenerateNewAuthenticatorKey();
    Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(dynamic user, int number);
    Task<string> GeneratePasswordResetTokenAsync(dynamic user);
    Task<string> GenerateTwoFactorTokenAsync(dynamic user, string tokenProvider);
    Task<string> GenerateUserTokenAsync(dynamic user, string tokenProvider, string purpose);
    Task<int> GetAccessFailedCountAsync(dynamic user);
    Task<string> GetAuthenticationTokenAsync(dynamic user, string loginProvider, string tokenName);
    Task<string> GetAuthenticatorKeyAsync(dynamic user);
    Task<IList<Claim>> GetClaimsAsync(dynamic user);
    Task<string> GetEmailAsync(dynamic user);
    Task<bool> GetLockoutEnabledAsync(dynamic user);
    Task<DateTimeOffset?> GetLockoutEndDateAsync(dynamic user);
    Task<IList<UserLoginInfo>> GetLoginsAsync(dynamic user);
    Task<string> GetPhoneNumberAsync(dynamic user);
    Task<IList<string>> GetRolesAsync(dynamic user);
    Task<string> GetSecurityStampAsync(dynamic user);
    Task<bool> GetTwoFactorEnabledAsync(dynamic user);
    Task<dynamic> GetUserAsync(ClaimsPrincipal principal);
    string GetUserId(ClaimsPrincipal principal);
    Task<string> GetUserIdAsync(dynamic user);
    string GetUserName(ClaimsPrincipal principal);
    Task<string> GetUserNameAsync(dynamic user);
    Task<IEnumerable<dynamic>> GetUsersForClaimAsync(Claim claim);
    Task<IEnumerable<dynamic>> GetUsersInRoleAsync(string roleName);
    Task<IList<string>> GetValidTwoFactorProvidersAsync(dynamic user);
    Task<bool> HasPasswordAsync(dynamic user);
    Task<bool> IsEmailConfirmedAsync(dynamic user);
    Task<bool> IsInRoleAsync(dynamic user, string role);
    Task<bool> IsLockedOutAsync(dynamic user);
    Task<bool> IsPhoneNumberConfirmedAsync(dynamic user);
    string NormalizeEmail(string email);
    string NormalizeName(string name);
    Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(dynamic user, string code);
    void RegisterTokenProvider(string providerName, dynamic provider);
    Task<IdentityResult> RemoveAuthenticationTokenAsync(dynamic user, string loginProvider, string tokenName);
    Task<IdentityResult> RemoveClaimAsync(dynamic user, Claim claim);
    Task<IdentityResult> RemoveClaimsAsync(dynamic user, IEnumerable<Claim> claims);
    Task<IdentityResult> RemoveFromRoleAsync(dynamic user, string role);
    Task<IdentityResult> RemoveFromRolesAsync(dynamic user, IEnumerable<string> roles);
    Task<IdentityResult> RemoveLoginAsync(dynamic user, string loginProvider, string providerKey);
    Task<IdentityResult> RemovePasswordAsync(dynamic user);
    Task<IdentityResult> ReplaceClaimAsync(dynamic user, Claim claim, Claim newClaim);
    Task<IdentityResult> ResetAccessFailedCountAsync(dynamic user);
    Task<IdentityResult> ResetAuthenticatorKeyAsync(dynamic user);
    Task<IdentityResult> ResetPasswordAsync(dynamic user, string token, string newPassword);
    Task<IdentityResult> SetAuthenticationTokenAsync(dynamic user, string loginProvider, string tokenName, string tokenValue);
    Task<IdentityResult> SetEmailAsync(dynamic user, string email);
    Task<IdentityResult> SetLockoutEnabledAsync(dynamic user, bool enabled);
    Task<IdentityResult> SetLockoutEndDateAsync(dynamic user, DateTimeOffset? lockoutEnd);
    Task<IdentityResult> SetPhoneNumberAsync(dynamic user, string phoneNumber);
    Task<IdentityResult> SetTwoFactorEnabledAsync(dynamic user, bool enabled);
    Task<IdentityResult> SetUserNameAsync(dynamic user, string userName);
    Task<IdentityResult> UpdateAsync(dynamic user);
    Task UpdateNormalizedEmailAsync(dynamic user);
    Task UpdateNormalizedUserNameAsync(dynamic user);
    Task<IdentityResult> UpdateSecurityStampAsync(dynamic user);
    Task<bool> VerifyChangePhoneNumberTokenAsync(dynamic user, string token, string phoneNumber);
    Task<bool> VerifyTwoFactorTokenAsync(dynamic user, string tokenProvider, string token);
    Task<bool> VerifyUserTokenAsync(dynamic user, string tokenProvider, string purpose, string token);
  }

  public class SDHCMemberService<T> : ISDHCMemberService where T : SDHCUser, new()
  {
    private readonly UserManager<T> u;
    public SDHCMemberService(UserManager<T> u)
    {
      this.u = u;
    }
    public IQueryable<dynamic> Users => this.u.Users;

    public IEnumerable<dynamic> UserValidators => this.u.UserValidators.Select(b => b as dynamic);

    public Task<IdentityResult> AccessFailedAsync(dynamic user)
    {
      return this.u.AccessFailedAsync(user);
    }

    public Task<IdentityResult> AddClaimAsync(dynamic user, Claim claim)
    {
      return u.AddClaimAsync(user, claim);
    }

    public Task<IdentityResult> AddClaimsAsync(dynamic user, IEnumerable<Claim> claims)
    {
      return u.AddClaimsAsync(user, claims);
    }

    public Task<IdentityResult> AddLoginAsync(dynamic user, UserLoginInfo login)
    {
      return u.AddLoginAsync(user, login);
    }

    public Task<IdentityResult> AddPasswordAsync(dynamic user, string password)
    {
      return u.AddPasswordAsync(user, password);
    }

    public Task<IdentityResult> AddToRoleAsync(dynamic user, string role)
    {
      return u.AddToRoleAsync(user, role);
    }

    public Task<IdentityResult> AddToRolesAsync(dynamic user, IEnumerable<string> roles)
    {
      return u.AddToRolesAsync(user, roles);
    }

    public Task<IdentityResult> ChangeEmailAsync(dynamic user, string newEmail, string token)
    {
      return u.ChangeEmailAsync(user, newEmail, token);
    }

    public Task<IdentityResult> ChangePasswordAsync(dynamic user, string currentPassword, string newPassword)
    {
      return u.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public Task<IdentityResult> ChangePhoneNumberAsync(dynamic user, string phoneNumber, string token)
    {
      return u.ChangePhoneNumberAsync(user, phoneNumber, token);
    }

    public Task<bool> CheckPasswordAsync(dynamic user, string password)
    {
      return u.CheckPasswordAsync(user, password);
    }

    public Task<IdentityResult> ConfirmEmailAsync(dynamic user, string token)
    {
      return u.ConfirmEmailAsync(user, token);
    }

    public Task<int> CountRecoveryCodesAsync(dynamic user)
    {
      return u.CountRecoveryCodesAsync(user);
    }

    public Task<IdentityResult> CreateAsync(dynamic user)
    {
      return u.CreateAsync(user);
    }

    public Task<IdentityResult> CreateAsync(dynamic user, string password)
    {
      return u.CreateAsync(user, password);
    }

    public Task<byte[]> CreateSecurityTokenAsync(dynamic user)
    {
      return u.CreateSecurityTokenAsync(user);
    }

    public Task<IdentityResult> DeleteAsync(dynamic user)
    {
      return u.DeleteAsync(user);
    }

    public void Dispose()
    {
      u.Dispose();
      this.Dispose();
    }

    public async Task<dynamic> FindByEmailAsync(string email)
    {
      return await u.FindByEmailAsync(email);
    }

    public async Task<dynamic> FindByIdAsync(string userId)
    {
      return await u.FindByIdAsync(userId);
    }

    public async Task<dynamic> FindByLoginAsync(string loginProvider, string providerKey)
    {
      return await u.FindByLoginAsync(loginProvider, providerKey);
    }

    public async Task<dynamic> FindByNameAsync(string userName)
    {
      return await u.FindByNameAsync(userName);
    }

    public async Task<dynamic> GetUserAsync(ClaimsPrincipal principal)
    {
      return await u.GetUserAsync(principal);
    }

    public string GetUserId(ClaimsPrincipal principal)
    {
      return u.GetUserId(principal);
    }

    public Task<string> GetUserIdAsync(dynamic user)
    {
      return u.GetUserIdAsync(user);
    }

    public string GetUserName(ClaimsPrincipal principal)
    {
      return u.GetUserName(principal);
    }

    public Task<string> GetUserNameAsync(dynamic user)
    {
      return u.GetUserNameAsync(user);
    }

    public async Task<IEnumerable<dynamic>> GetUsersForClaimAsync(Claim claim)
    {
      var list = await u.GetUsersForClaimAsync(claim);
      return list.Select(b => b as dynamic);
    }

    public async Task<IEnumerable<dynamic>> GetUsersInRoleAsync(string roleName)
    {
      var list = await u.GetUsersInRoleAsync(roleName);
      return list.Select(b => b as dynamic);
    }

    public Task<string> GenerateChangeEmailTokenAsync(dynamic user, string newEmail)
    {
      return u.GenerateChangeEmailTokenAsync(user, newEmail);
    }

    public Task<string> GenerateChangePhoneNumberTokenAsync(dynamic user, string phoneNumber)
    {
      return u.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
    }

    public Task<string> GenerateConcurrencyStampAsync(dynamic user)
    {
      return u.GenerateConcurrencyStampAsync(user);
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(dynamic user)
    {
      return u.GenerateEmailConfirmationTokenAsync(user);
    }

    public string GenerateNewAuthenticatorKey()
    {
      return u.GenerateNewAuthenticatorKey();
    }

    public Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(dynamic user, int number)
    {
      return u.GenerateNewTwoFactorRecoveryCodesAsync(user, number);
    }

    public Task<string> GeneratePasswordResetTokenAsync(dynamic user)
    {
      return u.GeneratePasswordResetTokenAsync(user);
    }

    public Task<string> GenerateTwoFactorTokenAsync(dynamic user, string tokenProvider)
    {
      return u.GenerateTwoFactorTokenAsync(user, tokenProvider);
    }

    public Task<string> GenerateUserTokenAsync(dynamic user, string tokenProvider, string purpose)
    {
      return u.GenerateUserTokenAsync(user, tokenProvider, purpose);
    }

    public Task<int> GetAccessFailedCountAsync(dynamic user)
    {
      return u.GetAccessFailedCountAsync(user);
    }

    public Task<string> GetAuthenticationTokenAsync(dynamic user, string loginProvider, string tokenName)
    {
      return u.GetAuthenticationTokenAsync(user, loginProvider, tokenName);
    }

    public Task<string> GetAuthenticatorKeyAsync(dynamic user)
    {
      return u.GetAuthenticatorKeyAsync(user);
    }

    public Task<IList<Claim>> GetClaimsAsync(dynamic user)
    {
      return u.GetClaimsAsync(user);
    }

    public Task<string> GetEmailAsync(dynamic user)
    {
      return u.GetEmailAsync(user);
    }

    public Task<bool> GetLockoutEnabledAsync(dynamic user)
    {
      return u.GetLockoutEnabledAsync(user);
    }

    public Task<DateTimeOffset?> GetLockoutEndDateAsync(dynamic user)
    {
      return u.GetLockoutEndDateAsync(user);
    }

    public Task<IList<UserLoginInfo>> GetLoginsAsync(dynamic user)
    {
      return u.GetLoginsAsync(user);
    }

    public Task<string> GetPhoneNumberAsync(dynamic user)
    {
      return u.GetPhoneNumberAsync(user);
    }

    public Task<IList<string>> GetRolesAsync(dynamic user)
    {
      return u.GetRolesAsync(user);
    }

    public Task<string> GetSecurityStampAsync(dynamic user)
    {
      return u.GetSecurityStampAsync(user);
    }

    public Task<bool> GetTwoFactorEnabledAsync(dynamic user)
    {
      return u.GetTwoFactorEnabledAsync(user);
    }

    public Task<IList<string>> GetValidTwoFactorProvidersAsync(dynamic user)
    {
      return u.GetValidTwoFactorProvidersAsync(user);
    }

    public Task<bool> HasPasswordAsync(dynamic user)
    {
      return u.HasPasswordAsync(user);
    }

    public Task<bool> IsEmailConfirmedAsync(dynamic user)
    {
      return u.IsEmailConfirmedAsync(user);
    }

    public Task<bool> IsInRoleAsync(dynamic user, string role)
    {
      return u.IsInRoleAsync(user, role);
    }

    public Task<bool> IsLockedOutAsync(dynamic user)
    {
      return u.IsLockedOutAsync(user);
    }

    public Task<bool> IsPhoneNumberConfirmedAsync(dynamic user)
    {
      return u.IsPhoneNumberConfirmedAsync(user);
    }

    public string NormalizeEmail(string email)
    {
      return u.NormalizeEmail(email);
    }

    public string NormalizeName(string name)
    {
      return u.NormalizeName(name);
    }

    public Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(dynamic user, string code)
    {
      return u.RedeemTwoFactorRecoveryCodeAsync(user, code);
    }

    public void RegisterTokenProvider(string providerName, dynamic provider)
    {
      u.RegisterTokenProvider(providerName, provider);
    }

    public Task<IdentityResult> RemoveAuthenticationTokenAsync(dynamic user, string loginProvider, string tokenName)
    {
      return u.RemoveAuthenticationTokenAsync(user, loginProvider, tokenName);
    }

    public Task<IdentityResult> RemoveClaimAsync(dynamic user, Claim claim)
    {
      return u.RemoveClaimAsync(user, claim);
    }

    public Task<IdentityResult> RemoveClaimsAsync(dynamic user, IEnumerable<Claim> claims)
    {
      return u.RemoveClaimsAsync(user, claims);
    }

    public Task<IdentityResult> RemoveFromRoleAsync(dynamic user, string role)
    {
      return u.RemoveFromRoleAsync(user, role);
    }

    public Task<IdentityResult> RemoveFromRolesAsync(dynamic user, IEnumerable<string> roles)
    {
      return u.RemoveFromRolesAsync(user, roles);
    }

    public Task<IdentityResult> RemoveLoginAsync(dynamic user, string loginProvider, string providerKey)
    {
      return u.RemoveLoginAsync(user, loginProvider, providerKey);
    }

    public Task<IdentityResult> RemovePasswordAsync(dynamic user)
    {
      return u.RemovePasswordAsync(user);
    }

    public Task<IdentityResult> ReplaceClaimAsync(dynamic user, Claim claim, Claim newClaim)
    {
      return u.ReplaceClaimAsync(user, claim, newClaim);
    }

    public Task<IdentityResult> ResetAccessFailedCountAsync(dynamic user)
    {
      return u.ResetAccessFailedCountAsync(user);
    }

    public Task<IdentityResult> ResetAuthenticatorKeyAsync(dynamic user)
    {
      return u.ResetAuthenticatorKeyAsync(user);
    }

    public Task<IdentityResult> ResetPasswordAsync(dynamic user, string token, string newPassword)
    {
      return u.ResetPasswordAsync(user, token, newPassword);
    }

    public Task<IdentityResult> SetUserNameAsync(dynamic user, string userName)
    {
      return u.SetUserNameAsync(user as T, userName);
    }

    public Task<IdentityResult> SetAuthenticationTokenAsync(dynamic user, string loginProvider, string tokenName, string tokenValue)
    {
      return u.SetAuthenticationTokenAsync(user, loginProvider, tokenName, tokenValue);
    }

    public Task<IdentityResult> SetEmailAsync(dynamic user, string email)
    {
      return u.SetEmailAsync(user, email);
    }

    public Task<IdentityResult> SetLockoutEnabledAsync(dynamic user, bool enabled)
    {
      return u.SetLockoutEnabledAsync(user, enabled);
    }

    public Task<IdentityResult> SetLockoutEndDateAsync(dynamic user, DateTimeOffset? lockoutEnd)
    {
      return u.SetLockoutEndDateAsync(user, lockoutEnd);
    }

    public Task<IdentityResult> SetPhoneNumberAsync(dynamic user, string phoneNumber)
    {
      return u.SetPhoneNumberAsync(user, phoneNumber);
    }

    public Task<IdentityResult> SetTwoFactorEnabledAsync(dynamic user, bool enabled)
    {
      return u.SetTwoFactorEnabledAsync(user, enabled);
    }

    public Task<IdentityResult> UpdateAsync(dynamic user)
    {
      return u.UpdateAsync(user);
    }

    public Task UpdateNormalizedEmailAsync(dynamic user)
    {
      return u.UpdateNormalizedEmailAsync(user);
    }

    public Task UpdateNormalizedUserNameAsync(dynamic user)
    {
      return u.UpdateNormalizedUserNameAsync(user);
    }

    public Task<IdentityResult> UpdateSecurityStampAsync(dynamic user)
    {
      return u.UpdateSecurityStampAsync(user);
    }

    public Task<bool> VerifyChangePhoneNumberTokenAsync(dynamic user, string token, string phoneNumber)
    {
      return u.VerifyChangePhoneNumberTokenAsync(user, token, phoneNumber);
    }

    public Task<bool> VerifyTwoFactorTokenAsync(dynamic user, string tokenProvider, string token)
    {
      return u.VerifyTwoFactorTokenAsync(user, tokenProvider, token);
    }

    public Task<bool> VerifyUserTokenAsync(dynamic user, string tokenProvider, string purpose, string token)
    {
      return u.VerifyUserTokenAsync(user, tokenProvider, purpose, token);
    }
  }

}
