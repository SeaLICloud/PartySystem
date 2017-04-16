using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Role
    {
        public class By : Specification<Role>
        {
            private readonly RoleIdentifier _role;

            public By(RoleIdentifier role)
            {
                _role = role;
            }

            public override Expression<Func<Role, bool>> IsSatisfiedBy()
            {
                return x => x.Id.RoleName == _role.RoleName;
            }
        }
    }
}