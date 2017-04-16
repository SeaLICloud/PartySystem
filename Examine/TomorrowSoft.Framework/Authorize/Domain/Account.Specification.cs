using System;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;
using NHibernate.Util;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Account
    {
        public class By : Specification<Account>
        {
            private readonly AccountIdentifier identifier;

            public By(AccountIdentifier identifier)
            {
                this.identifier = identifier;
            }

            public override Expression<Func<Account, bool>> IsSatisfiedBy()
            {
                return x => x.Id.UserName == identifier.UserName;
            }
        }

        public class ByRoleName : Specification<Account>
        {
            private readonly string _roleName;

            public ByRoleName(string roleName)
            {
                _roleName = roleName;
            }

            public override Expression<Func<Account, bool>> IsSatisfiedBy()
            {
                return x => x.Roles.Any(y=>y.Id.RoleName == _roleName);
            }
        }
    }
}