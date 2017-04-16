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
    public class Sql2008Factory : INHibernateFactory
    {
        public Configuration Configuration { get; private set; }

        private static bool _export;

        public Sql2008Factory( Assembly[] assemblies,string connectionString, bool export)
        {
            _export = export;
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            Configuration = Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(m =>
                              {
                                  foreach (var assembly in assemblies)
                                  {
                                      m.FluentMappings.AddFromAssembly(assembly);
                                  }
                              })
                .ExposeConfiguration(c => c.SetProperty(Environment.ReleaseConnections, "on_close"))
                .ExposeConfiguration(
                    c =>
                    c.SetProperty(Environment.CurrentSessionContextClass, "web"))
                .ExposeConfiguration(BuildSchema)
                .BuildConfiguration();
        }

        private static void BuildSchema(global::NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config).Execute(true, _export, false);
        }
    }
}