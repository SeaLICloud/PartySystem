using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class RunningNumberMap : BaseClassMap<RunningNumber>
    {
        public RunningNumberMap()
        {
            Table("Sys_RunningNumber");
            Component(x => x.Id, m => m.Map(x => x.Key).Column("[Key]").Unique());
            Map(x => x.LastNumber);
            Component(
                x => x.Mask,
                m =>
                    {
                        m.Map(x => x.Prefix);
                        m.Map(x => x.Time);
                        m.Map(x => x.Serial);
                    });
        }
    }
}