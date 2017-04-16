using System;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Authorize.Mapping
{
    public class RequestLogMap : BaseClassMap<RequestLog>
    {
        public RequestLogMap()
        {
            Table("Sys_RequestLog");
            Component(x => x.Identifier, m => 
                                             {
                                                 m.Map(y => y.SessionId).UniqueKey("RequestLogKey");
                                                 m.Map(y => y.Index).Column("[Index]").UniqueKey("RequestLogKey");
                                             });
            Map(x => x.Url);
            Map(x => x.Time);
            Map(x => x.HttpMethod);
            Map(x => x.UserIp);
            Map(x => x.UserName);
            Map(x => x.ActionParameters).CustomType("StringClob").CustomSqlType("TEXT");
        }
    }
}