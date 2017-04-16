using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Infrastructure.Data.SessionFactories
{
    public class SQLiteInMemoryFactory : INHibernateFactory
    {
        public SQLiteInMemoryFactory(Assembly[] assemblies)
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            Configuration = null;
            _sessionFactory = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                .Mappings(m =>
                              {
                                  foreach (var assembly in assemblies)
                                      m.FluentMappings.AddFromAssembly(assembly);
                              })
                .ExposeConfiguration(
                    c =>
                    c.SetProperty(Environment.CurrentSessionContextClass, "thread_static"))
                .ExposeConfiguration(cfg => Configuration = cfg)
                .BuildSessionFactory();
            _session = _sessionFactory.OpenSession();
            BuildSchema(Configuration, _session);
        }

        public Configuration Configuration { get; private set; }

        private readonly ISessionFactory _sessionFactory;

        private readonly ISession _session;

        private static void BuildSchema(Configuration config, ISession session)
        {
            new SchemaExport(config).Execute(true, true, false, session.Connection, null);
        }

        public ISession GetSession()
        {
            return _session;
        }

        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }
    }
}