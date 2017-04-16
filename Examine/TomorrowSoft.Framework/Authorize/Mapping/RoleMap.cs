using FluentNHibernate.Mapping;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class RoleMap : BaseClassMap<Role>
    {
        public RoleMap()
        {
            Table("Sys_Role");
            Component(x => x.Id, m => m.Map(y => y.RoleName)).Unique();
            Map(x => x.Description);
            Map(x => x.IsVisible);
            HasMany(x => x.RoleAuthorities).Cascade.AllDeleteOrphan();
        }
    }
}