using System;
using Microsoft.Practices.Unity;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Exceptions;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Keys;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Resources;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Framework.Infrastructure.Data.SessionFactories
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class Database
    {
        public static void InitTestDatabase()
        {
            var keys = IoC.Get<IConfigurationKeys>();
            var factory = new SQLiteInMemoryFactory(keys.ClassMapAssemblies);
            IoC.Current.RegisterInstance(factory.GetSession());
            IoC.Current.RegisterInstance(factory.GetSessionFactory());
            IoC.Current.RegisterType(typeof(IUnitOfWork), typeof(TestUnitOfWork));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            ConfigurationDatabase(true);
        }

        /// <summary>
        /// 准备
        /// </summary>
        public static void Prepare()
        {
            ConfigurationDatabase(false);
        }

        private static void ConfigurationDatabase(bool initDatabase)
        {
            IoC.Current.RegisterType(typeof(IUnitOfWork), typeof(UnitOfWork));

            var keys = IoC.Get<IConfigurationKeys>();

            if (keys.DataBaseType != DataBaseType.SQLiteInMemory)
            {
                if (keys.ConnectionString == null)
                    return;
            }

            try
            {
                INHibernateFactory factory = null;
                switch (keys.DataBaseType)
                {
                    case DataBaseType.SQLite:
                        factory = new SQLiteWebFactory(keys.ClassMapAssemblies, keys.ConnectionString, initDatabase);
                        break;

                    case DataBaseType.SQLServer2008:
                        factory = new Sql2008Factory(keys.ClassMapAssemblies, keys.ConnectionString, initDatabase);
                        break;

                    case DataBaseType.SQLiteThread:
                        factory = new SQLiteThreadFactory(keys.ClassMapAssemblies, keys.ConnectionString, initDatabase);
                        break;
                    default:
                        throw new FrameworkException("框架暂时仅支持SQLite和SQLServer2008数据库！");
                }
                if (!initDatabase)
                    IoC.Current.RegisterInstance(factory.Configuration.BuildSessionFactory());
            }
            catch (Exception e)
            {
                throw new FrameworkException(Messages.exception_ErrorDataBaseConfiguration + e.ToString());
            }
        }
    }
}