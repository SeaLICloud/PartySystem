using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class VersionMap : BaseClassMap<Version>
    {
        public VersionMap()
        {
            Table("Sys_Version");
            Map(x => x.Type).Column("[Type]");
            Map(x => x.VersionNumber);
            Map(x => x.SoftwareTitle);
        }
    }
}