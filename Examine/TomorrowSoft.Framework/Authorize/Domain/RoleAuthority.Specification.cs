using System;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class AccountAuthority
    {
        public class By : Specification<AccountAuthority>
        {
            private readonly AccountAuthorityIdentifier _accountAuthority;

            public By(AccountAuthorityIdentifier accountAuthority)
            {
                _accountAuthority = accountAuthority;
            }

            public override Expression<Func<AccountAuthority, bool>> IsSatisfiedBy()
            {
                return x => x.AccountId.UserName == _accountAuthority.AccountId.UserName &&
                            x.Function.DBID == _accountAuthority.FunctionId.DBID;
            }
        }
    }
}