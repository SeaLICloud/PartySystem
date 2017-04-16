using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class RoleAuthorityMap : BaseClassMap<RoleAuthority>
    {
        public RoleAuthorityMap()
        {
            Table("Sys_RoleAuthority");
            Component(x => x.RoleId, m=>m.Map(y=>y.RoleName).UniqueKey("RoleAuthorityUniqueKey"));
            References(x => x.Function).UniqueKey("RoleAuthorityUniqueKey");
            Map(x => x.IsAuthorized);
        }
    }
}