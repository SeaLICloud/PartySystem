using FluentNHibernate.Mapping;
using NHibernate;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Infrastructure.Data.SessionFactories
{
    public interface INHibernateFactory
    {
        global::NHibernate.Cfg.Configuration Configuration { get; }
    }
}