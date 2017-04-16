using System;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class RoleAuthority
    {
        public class ByRoles : Specification<RoleAuthority>
        {
            private readonly string[] _roles;

            public ByRoles(string[] roles)
            {
                _roles = roles;
            }

            public override Expression<Func<RoleAuthority, bool>> IsSatisfiedBy()
            {
                return x => _roles.Contains(x.RoleId.RoleName);
            }
        }

        public class By : Specification<RoleAuthority>
        {
            private readonly RoleAuthorityIdentifier _roleAuthority;

            public By(RoleAuthorityIdentifier roleAuthority)
            {
                _roleAuthority = roleAuthority;
            }

            public override Expression<Func<RoleAuthority, bool>> IsSatisfiedBy()
            {
                return x => x.RoleId.RoleName == _roleAuthority.RoleId.RoleName &&
                            x.Function.DBID == _roleAuthority.FunctionId.DBID;
            }
        }
    }
}