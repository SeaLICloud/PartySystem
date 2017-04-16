using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class FunctionMap : BaseClassMap<Function>
    {
        public FunctionMap()
        {
            Table("Sys_Function");
            Map(x => x.Area);
            Map(x => x.Controller);
            Map(x => x.Action);
            Map(x => x.Description);
            Map(x => x.MenuAction);
            Map(x => x.MenuDescription);
            Map(x => x.Group).Column("[Group]");
            Map(x => x.GroupIco);
        }
    }
}