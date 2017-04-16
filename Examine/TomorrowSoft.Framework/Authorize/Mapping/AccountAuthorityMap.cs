using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class AccountAuthorityMap : BaseClassMap<AccountAuthority>
    {
        public AccountAuthorityMap()
        {
            Table("Sys_AccountAuthority");
            Component(x => x.AccountId, m => m.Map(y => y.UserName).UniqueKey("AccountAuthorityUniqueKey"));
            References(x => x.Function).UniqueKey("AccountAuthorityUniqueKey");
            Map(x => x.IsAuthorized);
        }
    }
}