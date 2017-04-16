using System.Reflection;
using TomorrowSoft.Framework.Infrastructure.Data.SessionFactories;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Keys
{
    public interface IConfigurationKeys
    {
        /// <summary>
        /// 持久化层映射
        /// </summary>
        Assembly[] ClassMapAssemblies { get; }

        /// <summary>
        /// 数据库节
        /// </summary>
        DataBaseType DataBaseType { get; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 消息提示模式
        /// </summary>
        MessageMode MessageMode { get; }

        /// <summary>
        /// 每页显示记录条数
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 验收测试服务地址
        /// </summary>
        string AcceptanceTestServerAddress { get; }
    }
}