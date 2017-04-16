using FluentNHibernate.Mapping;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Infrastructure.Data.Repositories
{
    public class BaseClassMap<T> : ClassMap<T>
        where T : Entity<T>
    {
        public BaseClassMap()
        {
            Id(x => x.DBID).GeneratedBy.GuidComb();
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);
            Map(x => x.CreateUser);
            Map(x => x.UpdateUser);
            Map(x => x.Remark);
        }
    }
}