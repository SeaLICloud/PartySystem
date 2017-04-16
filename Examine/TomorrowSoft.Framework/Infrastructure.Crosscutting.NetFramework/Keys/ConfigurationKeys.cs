using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Exceptions;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Resources;
using TomorrowSoft.Framework.Infrastructure.Data.SessionFactories;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Keys
{
    [RegisterToContainer]
    public class ConfigurationKeys : IConfigurationKeys
    {
        public Assembly[] ClassMapAssemblies
        {
            get
            {
                var classMapAssemblies = new List<Assembly>();
                string[] appKeys = ConfigurationManager.AppSettings.AllKeys;
                foreach (var appKey in appKeys)
                {
                    if (appKey.Contains("ClassMap"))
                    {
                        var classMapAssemblyNode = ConfigurationManager.AppSettings[appKey];
                        classMapAssemblies.Add(Assembly.Load(classMapAssemblyNode));
                    }
                }
                if (classMapAssemblies.Count == 0)
                    throw new FrameworkException("无法找到包含“ClassMap”的配置节");
                return classMapAssemblies.ToArray();
            }
        }

        public DataBaseType DataBaseType
        {
            get
            {
                 var dataBaseTypeString = ConfigurationManager.AppSettings["DataBaseType"];
                 DataBaseType dataBaseType;
                 if (Enum.IsDefined(typeof(DataBaseType), dataBaseTypeString))
                     dataBaseType = (DataBaseType)Enum.Parse(typeof(DataBaseType), dataBaseTypeString);
                 else
                     throw new FrameworkException("无法找到名为“DataBaseType”的配置节(SQLiteInMemory,SQLite,SQLServer2008)");
                return dataBaseType;
            }
        }

        public string ConnectionString
        {
            get { return ConfigurationManager.AppSettings["ConnectionString"]; }
        }

        public MessageMode MessageMode
        {
            get
            {
                var messageModeString = ConfigurationManager.AppSettings["MessageMode"];
                MessageMode messageMode;
                if (Enum.IsDefined(typeof(MessageMode), messageModeString))
                    messageMode = (MessageMode)Enum.Parse(typeof(MessageMode), messageModeString);
                else
                    throw new FrameworkException("“MessageMode”配置节错误，缺少该配置节，或者值不对(可选值仅限 Running 和 Debug)");
                return messageMode;
            }
        }

        public int PageSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]); }
        }

        public string AcceptanceTestServerAddress 
        {
            get { return ConfigurationManager.AppSettings["AcceptanceTestServerAddress"]; }
        }
    }

    public enum MessageMode
    {
        Debug,
        Running
    }
}