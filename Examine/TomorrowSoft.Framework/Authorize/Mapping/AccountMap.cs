using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class AccountMap : BaseClassMap<Account>
    {
        public AccountMap()
        {
            Table("Sys_Account");
            Component(x => x.Id, m => m.Map(y => y.UserName).Unique());
            Map(x => x.Password).CustomType<byte[]>().Length(24);
            Map(x => x.Email);
            Map(x => x.UseRoleAuthority);
            HasManyToMany(x => x.Roles).Table("Sys_RoleToAccount").Not.LazyLoad();
            HasMany(x => x.AccountAuthorities).Cascade.AllDeleteOrphan();
        }
    }
}