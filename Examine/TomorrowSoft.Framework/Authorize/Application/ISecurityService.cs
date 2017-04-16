using System.Collections.Generic;
using System.Web.Security;
using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface ISecurityService
    {
        #region Account
        IEnumerable<Account> GetAccounts();
        Account GetAccount(AccountIdentifier id);
        IAccountCommand CreateAccount(string userName);
        IAccountCommand UpdateAccount(AccountIdentifier id);
        void DeleteAccount(AccountIdentifier id);
        bool ValidateAccount(string userName, string password, out string errorMessage);
        bool IsDuplicateUserName(string userName);
        #endregion

        #region Role
        IEnumerable<Role> GetRoles();
        Role GetRole(RoleIdentifier id);
        IRoleCommand CreateRole(string roleName);
        IEnumerable<Account> GetAccountsByRole(string roleName);
        IRoleCommand UpdateRole(RoleIdentifier id);
        #endregion

        #region Authority
        IEnumerable<IAuthority> GetAuthorities();
        IEnumerable<IAuthority> GetAuthorities(string[] roles);
        IEnumerable<IAuthority> GetAuthorities(AccountIdentifier id);
        void ImportAuthorities(string data, params Role[] roles);
        string ChangeRoleAuthority(RoleAuthorityIdentifier id);
        RoleAuthority GetRoleAuthority(RoleAuthorityIdentifier id);
        string ChangeAccountAuthority(AccountAuthorityIdentifier id);
        AccountAuthority GetAccountAuthority(AccountAuthorityIdentifier id);
        IFunctionCommand CreateFunction();
        bool Permit(string userName, string area, string controller, string action);
        #endregion

        #region Log
        IRequestLogCommand CreateRequestLog(string sessionId);
        #endregion

        #region Version
        IVersionCommand CreateVersion(string type);
        IVersionCommand EditVersion(string type);
        Version GetVersionByType(string type);
        #endregion

        #region RunningNumber

        RunningNumber GetRunningNumber(string key);
        void CreateRunningNumber(string key, RunningNumberMask mask);

        #endregion

    }
}