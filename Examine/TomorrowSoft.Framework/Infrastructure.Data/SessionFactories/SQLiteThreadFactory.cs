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
    public class SQLiteThreadFactory : INHibernateFactory
    {
        public Configuration Configuration { get; private set; }

        private static bool _export;

        public SQLiteThreadFactory(Assembly[] assemblies, string connectionString, bool export)
        {
            _export = export;
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            Configuration = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(m =>
                              {
                                  foreach(var assembly in assemblies)
                                  {
                                      m.FluentMappings.AddFromAssembly(assembly);
                                  }
                              })
                .ExposeConfiguration(c => c.SetProperty(Environment.ReleaseConnections, "on_close"))
                .ExposeConfiguration(
                    c =>
                    c.SetProperty(Environment.CurrentSessionContextClass, "thread_static"))
                .ExposeConfiguration(BuildSchema)
                .BuildConfiguration();
        }

        private static void BuildSchema(global::NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config).Execute(false, _export, false);
        }
    }
}