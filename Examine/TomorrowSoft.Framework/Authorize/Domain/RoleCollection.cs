using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public static class RoleCollection
    {
        public static IEnumerable<IAuthority> GetAuthorities(this IEnumerable<Role> roles)
        {
            if (roles.Count() == 0)
                return new List<RoleAuthority>();
            var authorities = roles.FirstOrDefault().GetAuthorities().ToList();
            foreach (var role in roles)
            {
                var tempAuthorities = role.GetAuthorities().ToList();
                for (var i = 0; i < authorities.Count; i++)
                {
                    authorities[i].IsAuthorized = authorities[i].IsAuthorized || tempAuthorities[i].IsAuthorized;
                }
            }
            return authorities;
        }

        public static string GetRoleNames(this IEnumerable<Role> roles) 
        {
            if (roles == null || !roles.Any())
                return "";
            var sb = new StringBuilder();
            foreach (var role in roles)
            {
                sb.Append(role.Description).Append("；");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}