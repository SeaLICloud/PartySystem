using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Presentation.Mvc;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    [RegisterToContainer]
    public class FormsAuthenticationService : IAuthenticationService
    {
        public void Login(string userName, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(userName, rememberMe);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public void SaveDataAuthorities(IEnumerable<string> authorizations)
        {
            HttpContext.Current.Session.Add(FrameworkKeys.DataAuthorities, authorizations);
        }

        public void SaveRole(IEnumerable<Role> roles)
        {
            HttpContext.Current.Session.Add(FrameworkKeys.RoleName, roles);
        }

        public void SaveAuthorities(IEnumerable<IAuthority> authorities)
        {
            HttpContext.Current.Session.Add(FrameworkKeys.Authorities, authorities);
        }

        public IEnumerable<string> GetDataAuthorities()
        {
            return HttpContext.Current.Session[FrameworkKeys.DataAuthorities] as IEnumerable<string>;
        }

        public bool HasRole()
        {
            return HttpContext.Current.Session[FrameworkKeys.RoleName] != null;
        }

        public IEnumerable<Role> GetRole()
        {
            return HttpContext.Current.Session[FrameworkKeys.RoleName] as IEnumerable<Role>;
        }

        public string GetCurrentUser()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public bool HasAuthorities()
        {
            return HttpContext.Current.Session[FrameworkKeys.Authorities] != null;
        }

        public IEnumerable<IAuthority> GetAuthorities()
        {
            return HttpContext.Current.Session[FrameworkKeys.Authorities] as IEnumerable<IAuthority>;
        }
    }
}