using System.Collections.Generic;
using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface IAuthenticationService
    {
        void Login(string userName, bool rememberMe);
        void SignOut();
        void SaveDataAuthorities(IEnumerable<string> authorizations);
        void SaveRole(IEnumerable<Role> roles);
        void SaveAuthorities(IEnumerable<IAuthority> authorities);
        IEnumerable<string> GetDataAuthorities();
        bool HasRole();
        IEnumerable<Role> GetRole();
        string GetCurrentUser();
        bool HasAuthorities();
        IEnumerable<IAuthority> GetAuthorities();
    }
}