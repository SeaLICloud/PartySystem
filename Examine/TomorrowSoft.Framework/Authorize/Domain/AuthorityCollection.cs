using System.Collections.Generic;
using System.Linq;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public static class AuthorityCollection
    {
         public static IEnumerable<IAuthority> Clone(this IEnumerable<IAuthority> authorities, Role role)
         {
             return authorities
                 .Select(x => new RoleAuthority(role, x.Function) {IsAuthorized = x.IsAuthorized});
         }

        public static bool Permit(this IEnumerable<IAuthority> authorities, 
            string area, string controller, string action)
        {
            if (!authorities.Any())
                return false;

            //如果有精确到acton的权限，则以action为准
            var result = authorities.Where(x =>
                                                       x.Function.Area == area &&
                                                       x.Function.Controller == controller &&
                                                       x.Function.Action.Split(new[]{'|'}).Contains(action));
            if (result.Any())
                return result.Count(x=>x.IsAuthorized) > 0;

            //如果没有精确到action，找action为空的权限
            result = authorities.Where(x =>
                                                       x.Function.Area == area &&
                                                       x.Function.Controller == controller &&
                                                       x.Function.Action == "");
            if (result.Any())
                return result.Count(x => x.IsAuthorized) > 0;

            //如果没有找到action，也没有找到该controller为空的action，则没有权限
            return false;
        }
    }
}